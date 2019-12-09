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

  public NeuralNetworkBProp(NeuralNetworkBProp copyNetwork)
  {
    //deep copy layers
    this.layer = new int[copyNetwork.layer.Length];
    for (int i = 0; i < copyNetwork.layer.Length; i++)
    {
      this.layer[i] = copyNetwork.layer[i];
    }

    //creates neural layers
    layers = new Layer[copyNetwork.layer.Length - 1];

    for (int i = 0; i < copyNetwork.layers.Length; i++)
    {
      layers[i] = new Layer(copyNetwork.layers[i]);
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
    for (int i = layers.Length - 1; i >= 0; i--)
    {
      if (i == layers.Length - 1)
      {
        layers[i].BackPropOutput(expected);
      }
      else
      {
        layers[i].BackPropHidden(layers[i + 1].gamma, layers[i + 1].weights);
      }
    }
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

    float learningRate = 0.0392699f; // learning rate
    int numberOfInputs;  //# of neurons in previous layer
    int numberOfOutputs; //# of neurons in the current layer
    public float[] outputs; //last outputs of the layer
    public float[] inputs; //last inputs of the layer
    public float[,] weights; // current weights of the layer
    public float[,] weightsDelta; // calculated weights we use calculate the new current weights
    public float[] gamma; // gamma calculated from error and the gamma formula
    public float[] error; // difference between expected output and output of the layer


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

    public Layer(Layer copyLayer)
    {
      this.numberOfInputs = copyLayer.numberOfInputs;
      this.numberOfOutputs = copyLayer.numberOfOutputs;

      outputs = new float[numberOfOutputs];
      inputs = new float[numberOfInputs];
      weights = new float[numberOfOutputs, numberOfInputs];
      weightsDelta = new float[numberOfOutputs, numberOfInputs];
      gamma = new float[numberOfOutputs];
      error = new float[numberOfOutputs];
      CopyWeights(copyLayer);
      //InitilizeWeights();
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


    public void CopyWeights(Layer copyLayer)
    {
      for (int i = 0; i < numberOfOutputs; i++)
      {
        for (int j = 0; j < numberOfInputs; j++)
        {
          this.weights[i, j] = copyLayer.weights[i, j];
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
      for (int i = 0; i < numberOfOutputs; i++)
      {
        error[i] = outputs[i] - expected[i];
      }
      for (int i = 0; i < numberOfOutputs; i++)
      {
        gamma[i] = error[i] * TanHDer(outputs[i]); // gamma formula
      }
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
      for (int i = 0; i < numberOfOutputs; i++)
      {
        gamma[i] = 0;

        for (int j = 0; j < gammaForward.Length; j++)
        {
          gamma[i] += gammaForward[j] * weightsForward[j, i];
        }

        gamma[i] *= TanHDer(outputs[i]);
      }
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
