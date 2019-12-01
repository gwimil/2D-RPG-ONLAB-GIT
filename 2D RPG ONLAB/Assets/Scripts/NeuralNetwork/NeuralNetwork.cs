﻿using System;
using System.Collections.Generic;


/// <summary>
/// Neural Network C#
/// </summary>
public class NeuralNetwork : IComparable<NeuralNetwork>
{

  private int[] layers; // layers
  private float[][] neurons; // neuron matrix
  private float[][][] weights; // weight matrix
  private float fitness; // fitness of the network


  /// <summary>
  /// Initilizes neural network with random weights
  /// </summary>
  /// <param name="layers">layers to the neural network</param>
  public NeuralNetwork(int[] layers)
  {
    this.layers = new int[layers.Length];
    for (int i = 0; i < layers.Length; i++)
    {
      this.layers[i] = layers[i];
    }

    // generate matrix
    InitNeurons();
    InitWeights();
  }

  /// <summary>
  /// Deep copy constructor
  /// </summary>
  /// <param name="copyNetwork">Network deep copy</param>
  public NeuralNetwork(NeuralNetwork copyNetwork)
  {
    this.layers = new int[copyNetwork.layers.Length];
    for (int i = 0; i < copyNetwork.layers.Length; i++)
    {
      this.layers[i] = copyNetwork.layers[i];
    }

    InitNeurons();
    InitWeights();
    CopyWeights(copyNetwork.weights);
  }


  private void CopyWeights(float[][][] copyWeights)
  {
    for (int i = 0; i < weights.Length; i++)
    {
      for (int j = 0; j < weights[i].Length; j++)
      {
        for (int k = 0; k < weights[i][j].Length; k++)
        {
          weights[i][j][k] = copyWeights[i][j][k];
        }
      }
    }
  }


  /// <summary>
  /// Create neuron matrix
  /// </summary>
  private void InitNeurons()
  {
    // Neuron init
    List<float[]> neuronsList = new List<float[]>();

    for (int i = 0; i < layers.Length; i++) // run through all layers
    {
      neuronsList.Add(new float[layers[i]]); // add a layer to the neuron list
    }

    neurons = neuronsList.ToArray(); // convert list to array
  }


  /// <summary>
  /// Create weight matrix
  /// </summary>
  private void InitWeights()
  {
    List<float[][]> weightList = new List<float[][]>(); // weights list which will later be converted into a weights 3D array


    //Iterate over all neurons that have a weight connection
    for (int i = 1; i < layers.Length; i++)
    {
      List<float[]> layerWeightList = new List<float[]>(); // layer weight list for this current layer (will be converted to 2D array)

      int neuronsInPreviousLayer = layers[i - 1];

      for (int j = 0; j < neurons[i].Length; j++)
      {
        float[] neuronWeights = new float[neuronsInPreviousLayer]; // neurons weights

        // Iterate over all neurons in the previous layer and set the weights randomly between 0.5f and -0.5f
        for (int k = 0; k < neuronsInPreviousLayer; k++)
        {
          //give random weights to neuron weights
          neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
        }

        layerWeightList.Add(neuronWeights); // add neuron weights of this current layer to weights 
      }
      weightList.Add(layerWeightList.ToArray()); // add this layer weights converted into 2D array into weights list
    }

    weights = weightList.ToArray(); //Convert to 3D array

  }


  /// <summary>
  /// Feed forward this neural network with a given input array
  /// </summary>
  /// <param name="inputs">Inputs to network</param>
  /// <returns></returns>
  public float[] FeedForward(float[] inputs)
  {
    // Add inputs to the neuron matrix
    for (int i = 0; i < inputs.Length; i++)
    {
      neurons[0][i] = inputs[i];
    }

    // iterate over all neurons and compute feedforward values
    for (int i = 1; i < layers.Length; i++)
    {
      for (int j = 0; j < neurons[i].Length; j++)
      {
        float value = 0.25f; // CONSTANT BIAS

        for (int k = 0; k < neurons[i - 1].Length; k++)
        {
          value += weights[i - 1][j][k] * neurons[i - 1][k]; // sum of all weights connectiion of this neuron weight their values in previous layer
        }

        neurons[i][j] = (float)Math.Tanh(value); //Hyperbolic tangent activation
      }
    }

    return neurons[neurons.Length - 1]; // return output layer
  }

  /// <summary>
  /// Mutate neural network weights
  /// </summary>
  public void Mutate()
  {
    for (int i = 0; i < weights.Length; i++)
    {
      for (int j = 0; j < weights[i].Length; j++)
      {
        for (int k = 0; k < weights[i][j].Length; k++)
        {

          float weight = weights[i][j][k];

          //mutate weight value
          UnityEngine.Random.Range(0.0f, 1.0f);
          float randomNumber = UnityEngine.Random.Range(0.0f, 1.0f) * 1000f;

          if (randomNumber <= 2f)
          {
            //if 1 flip sign of weight
            weight *= -1f;
          }
          else if (randomNumber <= 4f)
          {
            // if 2 pick a random weight between -1 and 1
            weight = UnityEngine.Random.Range(-0.5f, 0.5f);
          }
          else if (randomNumber <= 6f)
          {
            // if 3 randomly increase by 0% to 100%
            float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
            weight *= factor;
          }
          else if (randomNumber <= 8f)
          {
            // if 4 randomly decreasy by 0% to 100%
            float factor = UnityEngine.Random.Range(0f, 1f);
            weight *= factor;
          }
          weights[i][j][k] = weight;
        }
      }
    }
  }

  public void AddFitness(float fit)
  {
    fitness += fit;
  }

  public void setFitness(float fit)
  {
    fitness = fit;
  }

  public float GetFitenss()
  {
    return fitness;
  }


  /// <summary>
  /// Compare two neural networks and sort based on fitness
  /// </summary>
  /// <param name="other">Network to be compared to</param>
  /// <returns></returns>
  public int CompareTo(NeuralNetwork other)
  {
    if (other == null)
    {
      return 1;
    }

    if (fitness > other.fitness)
    {
      return 1;
    }
    else if (fitness < other.fitness)
    {
      return -1;
    }
    else
    {
      return 0;
    }


  }
}
