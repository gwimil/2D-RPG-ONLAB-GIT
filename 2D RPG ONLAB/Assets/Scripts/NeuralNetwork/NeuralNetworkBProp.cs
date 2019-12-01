using System;

/// <summary>
/// Simple MLP Neural Network
/// </summary>
public class NeuralNetworkBProp
{

  int[] layer; //layer information
  Layer[] layers; //layers in the network

  /// <summary>
  /// Constructor setting up layers
  /// </summary>
  /// <param name="layer">Layers of this network</param>
  public NeuralNetworkBProp(int[] layer)
  {
    //deep copy layers
    this.layer = new int[layer.Length];
    for (int i = 0; i < layer.Length; i++)
    {
      this.layer[i] = layer[i];
    }

    //creates neural layers
    layers = new Layer[layer.Length - 1];

    for (int i = 0; i < layers.Length; i++)
    {
      layers[i] = new Layer(layer[i], layer[i + 1]);
    }
  }



  /// <summary>
  /// High level feedforward for this network
  /// </summary>
  /// <param name="inputs">Inputs to be feed forwared</param>
  /// <returns></returns>
  public float[] FeedForward(float[] inputs)
  {
    //feed forward
    layers[0].FeedForward(inputs);
    for (int i = 1; i < layers.Length; i++)
    {
      layers[i].FeedForward(layers[i - 1].outputs);
    }


    return layers[layers.Length - 1].outputs; //return output of last layer
  }


  /// <summary>
  /// High level back porpagation
  /// Note: It is expexted the one feed forward was done before this back prop.
  /// </summary>
  /// <param name="expected">The expected output form the last feedforward</param>
  public void BackProp(float[] expected)
  {

    // run over all layers backwards
    for (int i = layers.Length - 1; i >= 0; i--)
    {
      if (i == layers.Length - 1)
      {
        layers[i].BackPropOutput(expected); //back prop output
      }
      else
      {
        layers[i].BackPropHidden(layers[i + 1].gamma, layers[i + 1].weights);//back prop hidden
      }
    }

    //Update weights
    for (int i = 0; i < layers.Length; i++)
    {
      layers[i].UpdateWeights();
    }

  }



  /// <summary>
  /// Each individual layer in the ML{
  /// </summary>
  public class Layer
  {

    float learningRate = 0.0392699f;

    int numberOfInputs;  //# of neurons in previous layer
    int numberOfOutputs; //# of neurons in the current layer

    public float[] outputs;
    public float[] inputs;
    public float[,] weights;
    public float[,] weightsDelta;
    public float[] gamma;
    public float[] error;


    /// <summary>
    /// Constructor initilizes vaiour data structures
    /// </summary>
    /// <param name="numberOfInputs">Number of neurons in the previous layer</param>
    /// <param name="numberOfOuputs">Number of neurons in the current layer</param>
    public Layer(int numberOfInputs, int numberOfOutputs)
    {
      this.numberOfInputs = numberOfInputs;
      this.numberOfOutputs = numberOfOutputs;

      outputs = new float[numberOfOutputs];
      inputs = new float[numberOfInputs];
      weights = new float[numberOfOutputs, numberOfInputs];
      weightsDelta = new float[numberOfOutputs, numberOfInputs];
      gamma = new float[numberOfOutputs];
      error = new float[numberOfOutputs];

      InitilizeWeights();
    }


    /// <summary>
    /// Initilize weights between -0.5 and 0.5
    /// </summary>
    public void InitilizeWeights()
    {
      for (int i = 0; i < numberOfOutputs; i++)
      {
        for (int j = 0; j < numberOfInputs; j++)
        {
          this.weights[i, j] = UnityEngine.Random.Range(-0.5f, 0.5f);
        }
      }
    }


    /// <summary>
    /// Feedforward this layer with a given input
    /// </summary>
    /// <param name="inputs">The output values of the previous layer</param>
    /// <returns></returns>
    public float[] FeedForward(float[] inputs)
    {
      this.inputs = inputs; // keep shallow copy which can be used for back propagation

      for (int i = 0; i < numberOfOutputs; i++)
      {
        outputs[i] = 0;
        for (int j = 0; j < numberOfInputs; j++)
        {
          outputs[i] += inputs[j] * weights[i, j];
        }

        outputs[i] = (float)Math.Tanh(outputs[i]);
      }

      return outputs;
    }


    /// <summary>
    /// Return the derived of the TanH of a value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float TanHDer(float value)
    {
      return 1 - (value * value);
    }


    /// <summary>
    /// Back propagation for the output layer
    /// If the layer is outputlayer we call this
    /// If we care about only to train the outputlayer then we only run this
    /// </summary>
    /// <param name="expected">The expected output</param>
    public void BackPropOutput(float[] expected)
    {
      //Error dervative of the cost function
      for (int i = 0; i < numberOfOutputs; i++)
      {
        error[i] = outputs[i] - expected[i];
      }
      //Gamma calculation
      for (int i = 0; i < numberOfOutputs; i++)
      {
        gamma[i] = error[i] * TanHDer(outputs[i]); // gamma formula
      }

      //Caluclating detla weights
      for (int i = 0; i < numberOfOutputs; i++)
      {
        for (int j = 0; j < numberOfInputs; j++)
        {
          weightsDelta[i, j] = gamma[i] * inputs[j];
        }
      }

    }

    /// <summary>
    /// Back propagation for the hidden layers
    /// </summary>
    /// <param name="gammaForward">the gamma value of the forward layer</param>
    /// <param name="weightsFoward">the weights of the forward layer</param>
    public void BackPropHidden(float[] gammaForward, float[,] weightsForward)
    {

      //Caluclate new gamma using gamma sums of the forward layer
      for (int i = 0; i < numberOfOutputs; i++)
      {
        gamma[i] = 0;

        for (int j = 0; j < gammaForward.Length; j++)
        {
          gamma[i] += gammaForward[j] * weightsForward[j, i];
        }

        gamma[i] *= TanHDer(outputs[i]);
      }

      //Caluclating detla weights
      for (int i = 0; i < numberOfOutputs; i++)
      {
        for (int j = 0; j < numberOfInputs; j++)
        {
          weightsDelta[i, j] = gamma[i] * inputs[j];
        }
      }
    }


    /// <summary>
    /// Updating weights
    /// </summary>
    public void UpdateWeights()
    {
      for (int i = 0; i < numberOfOutputs; i++)
      {
        for (int j = 0; j < numberOfInputs; j++)
        {
          this.weights[i, j] -= weightsDelta[i, j] * learningRate;
        }
      }
    }

  }

}
