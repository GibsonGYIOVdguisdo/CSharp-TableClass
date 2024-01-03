class Table{
    private IDictionary<string, List<string>> data = new Dictionary<string, List<string>>();

    public void fromCSV(string filePath){
        IDictionary<string, List<string>> tempData = new Dictionary<string, List<string>>();
        string[] fileContent = File.ReadAllText(filePath).Split("\n");

        foreach(string field in fileContent[0].Split(",")){
            tempData[field] = new List<string>();
        }

        for (int i = 0; i < fileContent.Length; i++){
            string[] recordData = fileContent[i].Split(",");

            for (int j = 0; j < recordData.Length; j++){
                tempData[tempData.Keys.ToArray()[j]].Add(recordData[j]); 
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