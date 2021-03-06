﻿using System;
using System.Collections.Generic;


public class NeuralNetwork
{

  private int[] layers;
  private float[][] neurons;
  private float[][][] weights;


  public NeuralNetwork(int[] layers)
  {
    this.layers = new int[layers.Length];
    for (int i = 0; i < layers.Length; i++)
    {
      this.layers[i] = layers[i];
    }

    InitNeurons();
    InitWeights();
  }

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


  private void InitWeights()
  {
    List<float[][]> weightList = new List<float[][]>();

    for (int i = 1; i < layers.Length; i++)
    {
      List<float[]> layerWeightList = new List<float[]>();

      int neuronsInPreviousLayer = layers[i - 1];

      for (int j = 0; j < neurons[i].Length; j++)
      {
        float[] neuronWeights = new float[neuronsInPreviousLayer];
        for (int k = 0; k < neuronsInPreviousLayer; k++)
        {
          neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
        }

        layerWeightList.Add(neuronWeights);
      }
      weightList.Add(layerWeightList.ToArray());
    }
    weights = weightList.ToArray();
  }

  public float[] FeedForward(float[] inputs)
  {
    for (int i = 0; i < inputs.Length; i++)
    {
      neurons[0][i] = inputs[i];
    }
    for (int i = 1; i < layers.Length; i++)
    {
      for (int j = 0; j < neurons[i].Length; j++)
      {
        float value = 0.25f;

        for (int k = 0; k < neurons[i - 1].Length; k++)
        {
          value += weights[i - 1][j][k] * neurons[i - 1][k];
        }
        neurons[i][j] = (float)Math.Tanh(value);
      }
    }

    return neurons[neurons.Length - 1];
  }

  public void Mutate()
  {
    for (int i = 0; i < weights.Length; i++)
    {
      for (int j = 0; j < weights[i].Length; j++)
      {
        for (int k = 0; k < weights[i][j].Length; k++)
        {
          float weight = weights[i][j][k];
          float randomNumber = UnityEngine.Random.Range(0.0f, 1.0f) * 1000f;
          float chanceToMutate = 10.0f;
          if (randomNumber <= chanceToMutate)
          {
            int mutateType = UnityEngine.Random.Range(0, 5);
            switch (mutateType)
            {
              case 0:
                weight *= -1f;
                break;
              case 1:
                weight = UnityEngine.Random.Range(-0.5f, 0.5f);
                break;
              case 2:
                float factor = UnityEngine.Random.Range(0f, 1.5f) + 1f;
                weight *= factor;
                break;
              case 3:
                float factor2 = UnityEngine.Random.Range(-0.5f, 1f);
                weight *= factor2;
                break;
              case 4:
                weight *= 1.392699f;
                break;
            }
          }
          weights[i][j][k] = weight;
        }
      }
    }
  }
}
