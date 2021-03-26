using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DBController : MonoBehaviour
{
    private string dbname = "URI=file:PlayerRecord.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();

        /*db testing only - shall be delete from this script*/
        AddRecord("eric", 15.5f, 25);
        AddRecord("bailey", 18.5f, 30);
        DisplayRecordsByPriority();
        /*----------------------------------------------*/
    }

    void CreateDB()
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS records (player VARCHAR(20), time REAL, points INTEGER);";
                command.ExecuteNonQuery();
            }
            

            connection.Close();
        }
    }

    public void AddRecord(string player, float time, int point)
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO records (player, time, points) VALUES ('" + player + "','" + time + "','" + point + "') ;";
                command.ExecuteNonQuery();

            }

            connection.Close();
        }
    }

    public void DisplayRecordsByAddedTime()
    {
        using(var connection = new SqliteConnection(dbname))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM records;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Debug.Log("Player: " + reader["player"] + " | time: " + reader["time"] + " | points: " + reader["points"]);

                        
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

    public void DisplayRecordsByPriority()
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM records ORDER BY CAST(points AS INTEGER) DESC, CAST(time AS REAL) DESC ;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("Player: " + reader["player"] + " | time: " + reader["time"] + " | points: " + reader["points"]);


                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
    }

}
