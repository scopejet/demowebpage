using System;
using System.Collections.Generic;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// Summary description for ClassName
/// </summary>
/// 
// is type disposable
public class ContactLogRepository : IDisposable
{
    // read only so it can't be changed, also passing it in in case connectionstring ever changes to diff type of db or credentials
    private readonly string _connectionString;
    private readonly Database _db;

    public ContactLogRepository(string connectionString)
    {
        _connectionString = connectionString;
        _db = Database.Open(connectionString);
    }

    public void Insert(string name, string email, string subject, string message, DateTime? date = null) {
       
        // must set datetime.now here and set to null above in 23 because datetime.now is not a compiled time constant, and that is needed for optional default parameters
        if (!date.HasValue) {
            date = DateTime.Now;
        }

        _db.Execute("INSERT INTO ContactLog (Name, Email, Subject, Message, DateSent) VALUES (@0, @1, @2, @3, @4)", 
            name, email, subject, message, date);
    }

    public IEnumerable<dynamic> GetAll() {
        return _db.Query("SELECT * FROM ContactLog ORDER BY DateSent DESC");
    }

    // in try catch since it may already be disposed.
    public void Dispose() {
        if (_db == null) return;

        try {
            _db.Dispose();
        } catch {}
    }
}
