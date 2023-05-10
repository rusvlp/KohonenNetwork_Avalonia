namespace KohonenNetwork.Network;

public static class TrainingSample
{
    public static double[][] IBMSet;

    public static void GenerateIBMSet(int size)
    {
        double[][] resultSet = new double[size][];
        int index = 0;
        int step = 2;
        for (double height = 153; height < size + 153; height += step)
        {
            for (double weight = 45; weight < size + 45 ; weight += step)
            {
                if (index >= size)
                {
                    break;
                }
                double ibm = weight / (height / 100) * (height / 100);
                resultSet[index] = new double[3];
                resultSet[index][0] = weight;
                resultSet[index][1] = height;
                resultSet[index][2] = ibm;
                index++;
            }
        }

        IBMSet =  resultSet;
    }
}