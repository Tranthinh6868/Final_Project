using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ClosedXML.Excel;
using Final_Project;


public class HistoryRepository
{
    private const string ExcelFilePath = "History.xlsx";
    private const string WorksheetName = "History";

    // Serialize danh sách Phép tính thành JSON byte array
    private static byte[] SerializeListHistory(List<History> his)
    {
        return JsonSerializer.SerializeToUtf8Bytes(his);
    }

    // Serialize một History thành JSON byte array
    private static byte[] SerializeHistory(History history)
    {
        List<History> tempList = new List<History> { history };
        return JsonSerializer.SerializeToUtf8Bytes(tempList);
    }

    // Deserialize JSON byte array thành danh sách History
    private static List<History> DeserializeHistory(byte[] data)
    {
        if (data == null || data.Length == 0)
        {
            return new List<History>(); // Trả về danh sách rỗng nếu dữ liệu đầu vào không hợp lệ
        }

        List<History> tasks = JsonSerializer.Deserialize<List<History>>(data);

        if (tasks == null)
        {
            return new List<History>(); // Trả về danh sách rỗng nếu quá trình giải tuần tự hóa thất bại
        }

        return tasks;
    }

    // Ghi danh sách phép tính vào Excel (Thêm mới, không ghi đè)
    public String SaveToExcel(List<History> his)
    {
        if (his == null || his.Count == 0)
        {
            return "Not found";
        }

        XLWorkbook workbook = LoadOrCreateWorkbook();
        IXLWorksheet worksheet = GetOrCreateWorksheet(workbook);

        try
        {
            int lastRow = 0;
            IXLRow lastRowUsed = worksheet.LastRowUsed();
            if (lastRowUsed != null)
            {
                lastRow = lastRowUsed.RowNumber();
            }

            byte[] serializedData = SerializeListHistory(his);
            worksheet.Cell(lastRow + 1, 1).Value = Convert.ToBase64String(serializedData);
            workbook.SaveAs(ExcelFilePath);
            return "Save success";
        }
        catch (Exception ex)
        {
            throw new IOException($"Lỗi khi lưu danh sách task dùng vào Excel: {ex.Message}", ex);
        }
    }


    // Ghi một History vào Excel
    public String SaveToExcel(History newHistory)
    {
        if (newHistory == null) throw new ArgumentNullException(nameof(newHistory));
        List<History> tempList = GetAllHistory();
        
        XLWorkbook workbook = LoadOrCreateWorkbook();
        IXLWorksheet worksheet = GetOrCreateWorksheet(workbook);

        try
        {
            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;
            byte[] serializedData = SerializeHistory(newHistory);
            worksheet.Cell(lastRow + 1, 1).Value = Convert.ToBase64String(serializedData);
            workbook.SaveAs(ExcelFilePath);
            return "Save success";
        }
        catch (Exception ex)
        {
            throw new IOException($"Lỗi khi lưu người dùng vào Excel: {ex.Message}", ex);
        }
    }
    private XLWorkbook LoadOrCreateWorkbook()
    {
        // Kiểm tra xem tệp Excel có tồn tại hay không
        if (File.Exists(ExcelFilePath))
        {
            // Nếu tệp tồn tại, tải workbook từ đường dẫn ExcelFilePath
            return new XLWorkbook(ExcelFilePath);
        }
        else
        {
            // Nếu tệp không tồn tại, tạo một workbook mới
            return new XLWorkbook();
        }

    }
    // Đọc danh sách History từ Excel
    public List<History> GetAllHistory()
    {
        if (!File.Exists(ExcelFilePath)) return new List<History>();

        XLWorkbook workbook = null;
        try
        {
            workbook = new XLWorkbook(ExcelFilePath);
            IXLWorksheet worksheet = workbook.Worksheet("History");
            List<History> allHistory = new List<History>();

            foreach (IXLRow row in worksheet.RowsUsed())
            {
                //if (row.RowNumber() == 1) continue; // Bỏ qua dòng tiêu đề
                string base64Data = row.Cell(1).GetString();
                if (base64Data != "")
                {
                    byte[] serializedData = Convert.FromBase64String(base64Data);
                    List<History> deserializedHistory = DeserializeHistory(serializedData);
                    for (int i = 0; i < deserializedHistory.Count; i++)
                        allHistory.Add(deserializedHistory[i]);
                }
            }
            return allHistory;
        }
        finally
        {
            if (workbook != null) workbook.Dispose();  //đóng file
        }
    }

   
    

    

    // Helper: Load hoặc tạo mới workbook
    private IXLWorksheet GetOrCreateWorksheet(XLWorkbook workbook)
    {
        IXLWorksheet foundWorksheet = null;

        foreach (var ws in workbook.Worksheets)
        {
            if (ws.Name == WorksheetName)
            {
                foundWorksheet = ws;
                break;
            }
        }

        if (foundWorksheet == null)
        {
            foundWorksheet = workbook.Worksheets.Add(WorksheetName);
        }

        return foundWorksheet;
    }

    

    public List<History> GetHistoryAround3Day(DateTime date)
    {
        List<History> his = GetAllHistory();
        List<History> temp = new List<History>();
        DateTime dateTimeNow = DateTime.Now;
        foreach (History h in his)
        {
            TimeSpan difference = dateTimeNow - date;
            if ((int)difference.TotalDays  <= 3)
            {
                temp.Add(h);
            }
        }
        return temp;
    }
  
}




