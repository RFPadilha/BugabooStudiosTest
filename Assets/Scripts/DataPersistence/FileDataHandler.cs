using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        //Path.Combine leva em considera��o o formato de destino de cada sistema operacional
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();//l� arquivo
                    }
                }
                //deserializa dados ap�s leitura
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading data from file: {fullPath}\n{e}");
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        //Path.Combine leva em considera��o o formato de destino de cada sistema operacional
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //cria diret�rio se n�o existir
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serializa dados
            string dataToStore = JsonUtility.ToJson(data, true);

            //"using" garante que a conex�o ao arquivo � fechada ao fim do escopo da fun��o
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);//escreve arquivo
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error when saving data to file: {fullPath}\n{e}");
        }
    }
}
