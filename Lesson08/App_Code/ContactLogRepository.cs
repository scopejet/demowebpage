using System;
using System.Collections.Generic;
using System.Web;
using WebMatrix.Data;

/// <summary>
/// Summary description for ClassName
/// </summary>
public class ContactLogRepository : IDisposable
{
    private readonly string _connectionString;
    private readonly Database _db;

    public ContactLogRepository(string connectionString)
    {
        _connectionString = connectionString;
        _db = Database.Open(connectionString);
    }

    public void Insert(string name, string email, string subject, string message, DateTime? date = null) {
        
        if (!date.HasValue) {
            date = DateTime.Now;
        }

        _db.Execute("INSERT INTO ContactLog (Name, Email, Subject, Message, DateSent) VALUES (@0, @1, @2, @3, @4)", 
            name, email, subject, message, date);
    }

    public IEnumerable<dynamic> GetAll() {
        return _db.Query("SELECT * FROM ContactLog ORDER BY DateSent DESC");
    }

    public void Dispose() {
        if (_db == null) return;

        try {
            _db.Dispose();
        } catch {}
    }
}
