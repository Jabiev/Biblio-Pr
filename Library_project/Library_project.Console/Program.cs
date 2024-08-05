using Library_project.Business.Services;
using Library_project.Core.Entities;
using System.Xml.Linq;

Console.BackgroundColor = ConsoleColor.White; Console.ForegroundColor = ConsoleColor.DarkYellow;
string[] messages = {
            "                                 ",
            " Welcome to the National Library ",
            "                                 "};
Console.WriteLine(new string(':', 37));
foreach (var message in messages)
    Console.WriteLine($"::{message}::");
Console.WriteLine(new string(':', 37));
Console.ResetColor();

AuthorService authorService = new();
GenreService genreService = new();
BookService bookService = new(authorService, genreService);
RenterService renterService = new();
LoanService loanService = new(bookService, renterService);

bool check = true;
while (check)
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\nPlease Choose One of The Following Options");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write(">1<---Publish Book\t>2<---Create Author\t>3<---Create Genre\t>4<---Loan book\n>5<---Update Book\t" +
    ">6<---Update Author\t>7<---Update Genre\t>8<---Return Book\n>9<---Delete Book\t>10<---Delete Author\t>11<---Delete Genre\t" +
    ">12<---All Books\n>13<---All Authors\t>14<---All Genres\t>15<---Overdue Loans\t>16<---Loaning Books\n>17<---Search Author\t" +
    ">18<---Search Genre\t>19<---Search Book\t|OPTIONAL| >20<---for more than one Renter\n\t\t\t\t\t");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(">100<---EXIT\n");
    Console.ResetColor();
    int option = default;
    try
    {
        option = int.Parse(Console.ReadLine());
    }
    catch (Exception)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("\tOoops,Please Enter only integer");
        Console.ResetColor();
    }
    switch (option)
    {
        case 1:
            if (authorService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Author, because of must being that");
                Console.ResetColor();
                goto case 2;
            }
            if (genreService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Genre, because of must being that");
                Console.ResetColor();
                goto case 3;
            }
            try
            {
                Console.Write("Please Enter Name of Book : ");
                string name = Console.ReadLine();
                Console.Write("& Publish Time(YEAR/MONTH/DAY): ");
                DateTime pT = DateTime.Parse(Console.ReadLine());
                Console.Write("& Count : ");
                int count = int.Parse(Console.ReadLine());
                Console.WriteLine("\t" + string.Join("\n\t", authorService.GetAll()));
                Console.Write("& Author Id(s) if more than 1 WITH , : ");
                HashSet<int> seta = new HashSet<int>(Console.ReadLine().Split(',').Select(int.Parse));
                Console.WriteLine("\t" + string.Join("\n\t", genreService.GetAll()));
                Console.Write("& Genre Id(s) if more than 1 WITH , : ");
                HashSet<int> setg = new HashSet<int>(Console.ReadLine().Split(',').Select(int.Parse));
                bookService.Create(name, pT, count, seta, setg);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 1;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 2:
            try
            {
                Console.Write("Please Enter Author's Name : ");
                string aName = Console.ReadLine();
                Console.Write("Please Enter Author's Surname : ");
                string surName = Console.ReadLine();
                authorService.Create(aName, surName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 2;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 3:
            try
            {
                Console.Write("Please Enter Name of Genre : ");
                string gName = Console.ReadLine();
                genreService.Create(gName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 3;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 4:
            if (bookService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Book, because of must being that");
                Console.ResetColor();
                goto case 1;
            }
            if (renterService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily now Creating Renter, because of must being that");
                Console.ResetColor();
                goto case 20;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", renterService.GetAll()));
                Console.Write("Please Copy and Paste Renter Id: ");
                Guid guid = Guid.Parse(Console.ReadLine());
                Console.WriteLine("\t" + string.Join("\n\t", bookService.GetAll()));
                Console.Write("Please Copy and Paste Book Id: ");
                Guid bGuid = Guid.Parse(Console.ReadLine());
                Console.Write("& Loan Date(YEAR/MONTH/DAY): ");
                DateTime lD = DateTime.Parse(Console.ReadLine());
                Console.Write("& Loan Period(only DAY that <= 7): ");
                double lP = double.Parse(Console.ReadLine());
                loanService.LoanBook(bGuid, guid, lD, lP);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                if (ex.Message == "The book count used up")
                    goto case 1;
                goto case 4;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 5:
            if (bookService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Book, because of must being that");
                Console.ResetColor();
                goto case 1;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", bookService.GetAll()));
                Console.Write("Please Copy and Paste Book Id: ");
                Guid eid = Guid.Parse(Console.ReadLine());
                Console.Write("Please Enter new Name of Book : ");
                string nname = Console.ReadLine();
                Console.Write("& new Publish Time(YEAR/MONTH/DAY): ");
                DateTime npT = DateTime.Parse(Console.ReadLine());
                Console.Write("& new Count : ");
                int ncount = int.Parse(Console.ReadLine());
                Console.WriteLine("\t" + string.Join("\n\t", authorService.GetAll()));
                Console.Write("& new Author Id(s) if more than 1 WITH , : ");
                HashSet<int> nseta = new HashSet<int>(Console.ReadLine().Split(',').Select(int.Parse));
                Console.WriteLine("\t" + string.Join("\n\t", genreService.GetAll()));
                Console.Write("& new Genre Id(s) if more than 1 WITH , : ");
                HashSet<int> nsetg = new HashSet<int>(Console.ReadLine().Split(',').Select(int.Parse));
                bookService.Update(eid, nname, npT, ncount, nseta, nsetg);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 6:
            if (authorService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Author, because of must being that");
                Console.ResetColor();
                goto case 2;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", authorService.GetAll()));
                Console.Write("Please Enter the Author Id you would like to change: ");
                int i_d = int.Parse(Console.ReadLine());
                Console.Write("Please Enter new Author's Name : ");
                string n_name = Console.ReadLine();
                Console.Write("Please Enter new Author's Surname : ");
                string n_sname = Console.ReadLine();
                authorService.Update(i_d, n_name, n_sname);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 6;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 7:
            if (genreService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Genre, because of must being that");
                Console.ResetColor();
                goto case 3;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", genreService.GetAll()));
                Console.Write("Please Enter the Genre Id you would like to change: ");
                int id_g = int.Parse(Console.ReadLine());
                Console.Write("Please Enter new Name of Genre : ");
                string n_nameg = Console.ReadLine();
                genreService.Update(id_g, n_nameg);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 7;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
            break;
        case 8:
            if (bookService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Book, because of must being that");
                Console.ResetColor();
                goto case 1;
            }
            if (loanService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily now Loan Book, because of had not been found loaning book");
                Console.ResetColor();
                goto case 4;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", renterService.GetAll()));
                Console.Write("Please Copy and Paste Renter Id: ");
                Guid gui_d = Guid.Parse(Console.ReadLine());
                Console.WriteLine("\t" + string.Join("\n\t", bookService.GetAll()));
                Console.Write("Please Copy and Paste Book Id: ");
                Guid bG_uid = Guid.Parse(Console.ReadLine());
                Console.Write("& Return Date(YEAR/MONTH/DAY): ");
                DateTime rD = DateTime.Parse(Console.ReadLine());
                loanService.ReturnBook(bG_uid, gui_d, rD);

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (ex.Message == "The value is anymore past-due")
                    Console.WriteLine("\tOoops, You owe 50$ because of you lated returning book time\n\t operation fulfilmented but with 50$ debt " +
                        "and restarting");
                else
                {
                    Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}\n\tand restarting");
                    Console.ResetColor();
                    goto case 8;
                }
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 9:
            if (bookService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Book, because of must being that");
                Console.ResetColor();
                goto case 1;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", bookService.GetAll()));
                Console.Write("Please Copy and Paste Book Id: ");
                Guid ibd = Guid.Parse(Console.ReadLine());
                bookService.Delete(ibd);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 9;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 10:
            if (authorService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Author, because of must being that");
                Console.ResetColor();
                goto case 2;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", authorService.GetAll()));
                Console.Write("Please Copy and Paste the Author Id you would like to delete : ");
                int auid = int.Parse(Console.ReadLine());
                authorService.Delete(auid, bookService);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 11:
            if (genreService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Genre, because of must being that");
                Console.ResetColor();
                goto case 2;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", genreService.GetAll()));
                Console.Write("Please Copy and Paste the Genre Id you would like to delete : ");
                int gnid = int.Parse(Console.ReadLine());
                genreService.Delete(gnid, bookService);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 12:
            if (bookService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Book, because of must being that");
                Console.ResetColor();
                goto case 1;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", bookService.GetAll()));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 13:
            if (authorService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Author, because of must being that");
                Console.ResetColor();
                goto case 2;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", authorService.GetAll()));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 14:
            if (genreService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Genre, because of must being that");
                Console.ResetColor();
                goto case 3;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", genreService.GetAll()));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 15:
            if (loanService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Loan Book(s), because of must being that");
                Console.ResetColor();
                goto case 4;
            }
            try
            {
                Console.WriteLine("Please Enter Your Serching Day(YEAR/MONTH/DAY) or other : ");
                DateTime lg_D = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("\t" + string.Join("\n\t", loanService.GetOverdueLoans(lg_D)));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 16:
            if (loanService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Loan Book(s), because of must being that");
                Console.ResetColor();
                goto case 4;
            }
            try
            {
                Console.WriteLine("\t" + string.Join("\n\t", loanService.GetAll()));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 17:
            if (authorService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Author, because of must being that");
                Console.ResetColor();
                goto case 2;
            }
            try
            {
                Console.WriteLine("Please Enter any clue of name");
                var list = authorService.SearchByName(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green;
                if (list.Count == 0)
                    Console.WriteLine("\nThe list doesn't exist by the search value");
                Console.ResetColor();
                Console.WriteLine("\t" + string.Join("\n\t", list));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 18:
            if (genreService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Choose Create Genre, because of must being that");
                Console.ResetColor();
                goto case 3;
            }
            try
            {
                Console.WriteLine("Please Enter any clue of name");
                var glist = genreService.SearchByName(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green;
                if (glist.Count == 0)
                    Console.WriteLine("\nThe list doesn't exist by the search value");
                Console.ResetColor();
                Console.WriteLine("\t" + string.Join("\n\t", glist));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 19:
            if (bookService.GetAll().Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\tPlease preliminarily Publish Book, because of must being that");
                Console.ResetColor();
                goto case 1;
            }
            try
            {
                Console.WriteLine("Please Enter any clue of name");
                var blist = bookService.SearchByName(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green;
                if (blist.Count == 0)
                    Console.WriteLine("\nThe list doesn't exist by the search value");
                Console.ResetColor();
                Console.WriteLine("\t" + string.Join("\n\t", blist));
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case -5;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...");
            Console.ResetColor();
            break;
        case 20:
            try
            {
                Console.Write("Please Enter Renter's Name : ");
                string rName = Console.ReadLine();
                Console.Write("Please Enter Renter's Surname : ");
                string rSurname = Console.ReadLine();
                renterService.Create(rName, rSurname);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\tOoops, Please Enter proper values | Your problem: {ex.Message}");
                Console.ResetColor();
                goto case 20;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\tThe Operation is successfully fulfilmenting...\n\tNow Loan Operation...");
            Console.ResetColor();
            goto case 4;
        case -5:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tIncorrect Command or Operation. And so Restarting App...");
            Console.ResetColor();
            break;
        default:
            if (option != 100)
                goto case -5;
            check = false;
            break;
    }
}