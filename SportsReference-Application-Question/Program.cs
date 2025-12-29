using System.Text.Json;

//Function that writes the word Team followed by all teams in the dictionary passed in 
void WriteTeams(Dictionary<string, Dictionary<string, Dictionary<string, int>>> data)
{
    Console.Write("Team");
    foreach (var team in data)
    {
        Console.Write($"{team.Key, 5}");
    }
    Console.Write("\n");
}

//Sample raw jsonString 
string jsonString = @"
{

'BRO': {

    'BSN': { 'W': 10, 'L': 12 },

    'CHC': { 'W': 15, 'L': 7 },

    'CIN': { 'W': 15, 'L': 7 },

    'NYG': { 'W': 14, 'L': 8 },

    'PHI': { 'W': 14, 'L': 8 },

    'PIT': { 'W': 15, 'L': 7 },

    'STL': { 'W': 11, 'L': 11 }

},

'BSN': {

    'BRO': { 'W': 12, 'L': 10 },

    'CHC': { 'W': 13, 'L': 9 },

    'CIN': { 'W': 13, 'L': 9 },

    'NYG': { 'W': 13, 'L': 9 },

    'PHI': { 'W': 14, 'L': 8 },

    'PIT': { 'W': 12, 'L': 10 },

    'STL': { 'W': 9, 'L': 13 }

},

'CHC': {

    'BRO': { 'W': 7, 'L': 15 },

    'BSN': { 'W': 9, 'L': 13 },

    'CIN': { 'W': 12, 'L': 10 },

    'NYG': { 'W': 7, 'L': 15 },

    'PHI': { 'W': 16, 'L': 6 },

    'PIT': { 'W': 8, 'L': 14 },

    'STL': { 'W': 10, 'L': 12 }

},

'CIN': {

    'BRO': { 'W': 7, 'L': 15 },

    'BSN': { 'W': 9, 'L': 13 },

    'CHC': { 'W': 10, 'L': 12 },

    'NYG': { 'W': 13, 'L': 9 },

    'PHI': { 'W': 13, 'L': 9 },

    'PIT': { 'W': 13, 'L': 9 },

    'STL': { 'W': 8, 'L': 14 }

},

'NYG': {

    'BRO': { 'W': 8, 'L': 14 },

    'BSN': { 'W': 9, 'L': 13 },

    'CHC': { 'W': 15, 'L': 7 },

    'CIN': { 'W': 9, 'L': 13 },

    'PHI': { 'W': 12, 'L': 10 },

    'PIT': { 'W': 15, 'L': 7 },

    'STL': { 'W': 13, 'L': 9 }

},

'PHI': {

    'BRO': { 'W': 8, 'L': 14 },

    'BSN': { 'W': 8, 'L': 14 },

    'CHC': { 'W': 6, 'L': 16 },

    'CIN': { 'W': 9, 'L': 13 },

    'NYG': { 'W': 10, 'L': 12 },

    'PIT': { 'W': 13, 'L': 9 },

    'STL': { 'W': 8, 'L': 14 }

},

'PIT': {

    'BRO': { 'W': 7, 'L': 15 },

    'BSN': { 'W': 10, 'L': 12 },

    'CHC': { 'W': 14, 'L': 8 },

    'CIN': { 'W': 9, 'L': 13 },

    'NYG': { 'W': 7, 'L': 15 },

    'PHI': { 'W': 9, 'L': 13 },

    'STL': { 'W': 6, 'L': 16 }

},

'STL': {

    'BRO': { 'W': 11, 'L': 11 },

    'BSN': { 'W': 13, 'L': 9 },

    'CHC': { 'W': 12, 'L': 10 },

    'CIN': { 'W': 14, 'L': 8 },

    'NYG': { 'W': 9, 'L': 13 },

    'PHI': { 'W': 14, 'L': 8 },

    'PIT': { 'W': 16, 'L': 6 }

}

}";

//Replaces '' values with "" values as that is json format in c#
jsonString = jsonString.Replace("'", "\"");
var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, int>>>>(jsonString);

//if data is not read properly throw
if (data is null)
{
    throw new Exception("Invalid JSON string");
}

//since json does not include own team H2H against self, adds it as 0-0 (shows later as ---)
foreach (var team in data)
{
    var temp = new Dictionary<string, int>();
    temp.Add("W", 0);
    temp.Add("L", 0);
    data[team.Key].Add(team.Key, temp);
}

//Writes the columns titles at the top
WriteTeams(data);

//Writes in each row
foreach (var (team1Name, opp) in data)
{
    //Team Name
    Console.Write($"{team1Name, 3}");
    
        foreach (var (team2Name, _) in data)
        {
            //if the team is being compared to itself
            if (team1Name == team2Name) { Console.Write("   ---"); }

            //win column which is what our table cares about
            else
            {
                int numWins = opp[team2Name]["W"];
                Console.Write($"{numWins,5}");
            }
        }
    //adds a newline for the next team to be outputted on its own row
    Console.Write("\n");
}

//Writes the columns titles at the bottom
WriteTeams(data);
