using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public interface IUserDataAccess
{
    DataRow AuthenticateUser(string username, string password);
    bool RegisterUser(string username, string password);
}

public class DataAccessLayer
{
    private readonly string connectionString;

    public DataAccessLayer()
    {
        connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    }

    public DataRow AuthenticateUser(string username, string password)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT UserId, Username 
                          FROM Users 
                          WHERE Username = @Username 
                          AND Password = @Password";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }

    public bool RegisterUser(string username, string password)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@Username", username);

            con.Open();
            int userCount = (int)checkCmd.ExecuteScalar();

            if (userCount > 0)
            {
                return false;
            }

            string insertQuery = @"INSERT INTO Users (Username, Password) 
                            VALUES (@Username, @Password)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
            insertCmd.Parameters.AddWithValue("@Username", username);
            insertCmd.Parameters.AddWithValue("@Password", password);

            int rowsAffected = insertCmd.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }

    public DataTable GetTaskStatistics(int userId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"SELECT 
                        COUNT(*) AS TotalTasks,
                        SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) AS PendingTasks,
                        SUM(CASE WHEN Status = 'In Progress' THEN 1 ELSE 0 END) AS InProgressTasks,
                        SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedTasks
                        FROM Tasks 
                        WHERE UserId = @UserId";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }

    public DataTable GetUserTasks(int userId, string sortOption = "RecentFirst")
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string baseQuery = @"SELECT TaskId, Title, Status, CreatedDate 
                             FROM Tasks 
                             WHERE UserId = @UserId";

            string orderByClause;

            switch (sortOption)
            {
                case "OldestFirst":
                    orderByClause = "ORDER BY CreatedDate ASC";
                    break;
                case "StatusPendingFirst":
                    orderByClause = @"ORDER BY 
                                    CASE Status 
                                        WHEN 'Pending' THEN 0 
                                        WHEN 'In Progress' THEN 1 
                                        ELSE 2 
                                    END,
                                    CreatedDate DESC";
                    break;
                case "StatusCompletedFirst":
                    orderByClause = @"ORDER BY 
                                    CASE Status 
                                        WHEN 'Completed' THEN 0 
                                        WHEN 'In Progress' THEN 1 
                                        ELSE 2 
                                    END,
                                    CreatedDate DESC";
                    break;
                case "TitleAsc":
                    orderByClause = "ORDER BY Title ASC";
                    break;
                case "TitleDesc":
                    orderByClause = "ORDER BY Title DESC";
                    break;
                case "RecentFirst":
                default:
                    orderByClause = "ORDER BY CreatedDate DESC";
                    break;
            }

            string query = baseQuery + " " + orderByClause;

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }

    public DataRow GetTaskById(int taskId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Tasks WHERE TaskId = @TaskId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TaskId", taskId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }

    public bool CreateTask(int userId, string title, string description, string status)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"INSERT INTO Tasks (UserId, Title, Description, Status)
                        VALUES (@UserId, @Title, @Description, @Status)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Status", status);

            con.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool UpdateTask(int taskId, string title, string description, string status)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = @"UPDATE Tasks 
                        SET Title = @Title,
                            Description = @Description,
                            Status = @Status
                        WHERE TaskId = @TaskId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TaskId", taskId);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Status", status);

            con.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    public bool DeleteTask(int taskId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Tasks WHERE TaskId = @TaskId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@TaskId", taskId);

            con.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}