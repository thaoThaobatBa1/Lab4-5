using System.Security.AccessControl;

namespace Lab5
{
    public class SanPham
    {
        public string id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public float Gia { get; set; }
        public string MauSac { get; set; }
        public string KichThuoc { get; set; }
        public int Soluong { get; set; }

        public SanPham(string id,string ma, string ten, float gia, string mauSac, string kichThuoc, int soluong)
        {
            this.id = id;
            Ma = ma;
            Ten = ten;
            Gia = gia;
            MauSac = mauSac;
            KichThuoc = kichThuoc;
            Soluong = soluong;
        }
    }
    public class SanPhamSevice
    {
        public List<SanPham> SanPhams = new List<SanPham>(); 

        public void AddSanPham (SanPham sanPham)
        {
            if (sanPham.Soluong <= 0 || sanPham.Soluong >= 100)
                throw new ArgumentException("SoLuong must be a positive integer less than 100.");
            SanPhams.Add(sanPham);
        }

        public void EditSanPham(SanPham sanPham)
        {
            var EditSanPham = SanPhams.FirstOrDefault(a => a.id == sanPham.id);
            if (EditSanPham == null)
                throw new ArgumentException("SanPham not found.");
            EditSanPham.id = sanPham.id;
            EditSanPham.Ten = sanPham.Ten;
            EditSanPham.MauSac = sanPham.MauSac;
            EditSanPham.KichThuoc = sanPham.KichThuoc;
            EditSanPham.Gia = sanPham.Gia;
            EditSanPham.Soluong = sanPham.Soluong;
        }

        public void RemoveSanPham(SanPham sanPham)
        {
            var EditSanPham = SanPhams.FirstOrDefault(a => a.id == sanPham.id);
            SanPhams.Remove(EditSanPham);
        }


    }
    public class Tests
    {


        private SanPhamSevice _sv;
        [SetUp]
        public void Setup()
        {
            _sv = new SanPhamSevice();
        }

        [Test]
        public void AddSanPham_ValidInput_AddsSanPham()
        {
            var sanPham = new SanPham("1", "SP001", "Product 1", 100.0f, "Red", "M", 50);
            _sv.AddSanPham(sanPham);
            Assert.AreEqual(1, _sv.SanPhams.Count);
        }

        [Test]
        public void AddSanPham_NegativeSoLuong_ThrowsException()
        {
            var sanPham = new SanPham("2", "SP002", "Product 2", 150.0f, "Blue", "L", -1);
            Assert.Throws<ArgumentException>(() => _sv.AddSanPham(sanPham));
        }

        [Test]
        public void AddSanPham_ZeroSoLuong_ThrowsException()
        {
            var sanPham = new SanPham("3","SP003", "Product 3", 200.0f, "Green", "XL", 0);
            Assert.Throws<ArgumentException>(() => _sv.AddSanPham(sanPham));
        }

        [Test]
        public void AddSanPham_OverLimitSoLuong_ThrowsException()
        {
            var sanPham = new SanPham("4", "SP004", "Product 4", 250.0f, "Yellow", "S", 100);
            Assert.Throws<ArgumentException>(() => _sv.AddSanPham(sanPham));
        }

        [Test]
        public void AddSanPham_ValidInput_MultipleAdds()
        {
            _sv.AddSanPham(new SanPham("5", "SP005", "Product 5", 300.0f, "Black", "M", 99));
            _sv.AddSanPham(new SanPham("6", "SP006", "Product 6", 350.0f, "White", "L", 1));
            Assert.AreEqual(2, _sv.SanPhams.Count);
        }


        // test sua
        [Test]
        public void EditSanPham_ValidInput_EditsSanPham()
        {
            var sanPham = new SanPham("7", "SP007", "Product 7", 400.0f, "Purple", "M", 20);
            _sv.AddSanPham(sanPham);
            sanPham.Ten = "Updated Product 7";
            _sv.EditSanPham(sanPham);
            Assert.AreEqual("Updated Product 7", _sv.SanPhams.First().Ten);
        }

        [Test]
        public void EditSanPham_NonExistentMaSanPham_ThrowsException()
        {
            var sanPham2 = new SanPham("8", "SP008", "Product 8", 450.0f, "Pink", "S", 30);
            Assert.Throws<ArgumentException>(() => _sv.EditSanPham(sanPham2));
        }

        [Test]
        public void EditSanPham_ValidMaSanPham_UpdatesCorrectly()
        {
            var sanPham = new SanPham("9", "SP009", "Product 9", 500.0f, "Orange", "L", 10);
            _sv.AddSanPham(sanPham);
            sanPham.Gia = 550.0f;
            _sv.EditSanPham(sanPham);
            Assert.AreEqual(550.0f, _sv.SanPhams.First().Gia);
        }

        [Test]
        public void EditSanPham_ValidMaSanPham_UpdatesSoLuong()
        {
            var sanPham = new SanPham("10", "SP010", "Product 10", 600.0f, "Gray", "M", 15);
            _sv.AddSanPham(sanPham);
            sanPham.Soluong = 25;
            _sv.EditSanPham(sanPham);
            Assert.AreEqual(25, _sv.SanPhams.First().Soluong);
        }

        [Test]
        public void EditSanPham_ValidMaSanPham_UpdatesMultipleFields()
        {
            var sanPham = new SanPham("11", "SP011", "Product 11", 700.0f, "Cyan", "L", 5);
            _sv.AddSanPham(sanPham);
            sanPham.Gia = 750.0f;
            sanPham.Ten = "Updated Product 11";
            sanPham.Soluong = 10;
            _sv.EditSanPham(sanPham);
            Assert.AreEqual(750.0f, _sv.SanPhams.First().Gia);
            Assert.AreEqual("Updated Product 11", _sv.SanPhams.First().Ten);
            Assert.AreEqual(10, _sv.SanPhams.First().Soluong);
        }


    }
}