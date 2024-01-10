using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

class Table{
    private IDictionary<string, List<string>> data = new Dictionary<string, List<string>>();



    public string[] splitCsvLine(string line){
        bool dontSplit = false;
        string currentString = "";
        List<string> splitString = new List<string>();
        foreach(char l in line){
            if (l == '"' && currentString.Length == 0 && dontSplit == false){
                dontSplit = true;
            }
            else if (l == '"' && dontSplit == true){
                dontSplit = false;
            }
            if (l == ',' && dontSplit == false){
                splitString.Add(currentString);
                currentString = "";
            }
            else{
                currentString += l;
            }
        }
        if (currentString.Length>0){
            splitString.Add(currentString);
        }
        return splitString.ToArray();
    
    }
    public void fromCSV(string filePath){
        IDictionary<string, List<string>> tempData = new Dictionary<string, List<string>>();
        string[] fileContent = File.ReadAllText(filePath).Split("\n");

        List<string> fields = new List<string>();

        foreach(string field in fileContent[0].Split(",")){
            tempData[field] = new List<string>();
            fields.Add(field);
        }


        for (int i = 0; i < fileContent.Length; i++){
            string[] recordData = splitCsvLine(fileContent[i]);
            for (int j = 0; j < recordData.Length; j++){
                tempData[fields[j]].Add(recordData[j]);
            }
        }
        this.data = tempData;
    }

    public void printTable(int count = 20, bool showIndex = true){
        string[] keys = this.data.Keys.ToArray();
        int recordCount = this.data[keys[0]].Count;

        string toPrint = " ";

        for (int i = 0; i < recordCount && i < count; i++){
            foreach(string key in keys){
                toPrint += $" {this.data[key][i]}";
            }
            if (i != recordCount - 1 && i != count - 1 && showIndex == true){
                toPrint += $"\n{i}";
            }
        }
        Console.WriteLine(toPrint);
    }
    public string[] getRecord(int index){
        string[] keys = this.data.Keys.ToArray();
        string[] record = new string[this.data.Count];
        int count = 0;
        foreach(string field in keys){
            record[count] = this.data[field][index];
            count += 1;
        }
        return(record);
    }
}