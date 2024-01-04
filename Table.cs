class Table{
    private IDictionary<string, List<string>> data = new Dictionary<string, List<string>>();



    public string[] splitCsvLine(string line){
        bool dontSplit = false;
        string currentString = "";
        List<string> splitString = new List<string>();
        foreach(char l in line){
            if (l == '"' && splitString.Count == 0 && dontSplit == false){
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

        foreach(string field in fileContent[0].Split(",")){
            tempData[field] = new List<string>();
        }

        for (int i = 0; i < fileContent.Length; i++){
            string[] recordData = splitCsvLine(fileContent[i]);
            for (int j = 0; j < recordData.Length; j++){
                Console.WriteLine(recordData[j]);
            }
        }
        this.data = tempData;
    }

    public void printTable(){
        foreach (var field in this.data){
            Console.WriteLine(field.Key);
        }
    }
}