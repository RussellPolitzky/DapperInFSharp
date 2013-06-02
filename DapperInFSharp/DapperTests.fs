namespace DapperInFSharp

open Microsoft.VisualStudio.TestTools.UnitTesting    
open System.Data.SqlClient
open Dapper
open ImpromptuInterface.FSharp
open System.Text

[<CLIMutable>] 
type Person = { Name : string; Age : int }

[<TestClass>]
type DapperTests() =

    /// See the following URL for sample: https://gist.github.com/DotNetNerd/5538838
    [<TestMethod>]        
    member this.``should be able to run query with dapper using impromptu interface for dyanmics``() =
        let expected = "John,James,Jane,"
        use conn = new SqlConnection("Database=DapperTestDatabase;Trusted_Connection=True;") 
        conn.Open()
        let actual = 
            (conn.Query("SELECT * FROM dbo.Person")
            |> Seq.fold 
                (fun (strb:StringBuilder) person -> 
                    strb.Append((string)person?Name).Append(',')) 
                (new StringBuilder())).ToString()
        Assert.AreEqual(expected, actual)
                

