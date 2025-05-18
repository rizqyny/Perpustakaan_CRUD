using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Library perpustakaan = new Library("Perpustakaan Adrian");

        bool berjalan = true;
        while (berjalan)
        {
            Console.Clear();

            Console.WriteLine("\nMenu Perpustakaan");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Tampilkan Semua Buku");
            Console.WriteLine("3. Update Buku");
            Console.WriteLine("4. Hapus Buku");
            Console.WriteLine("5. Pinjam Buku");
            Console.WriteLine("6. Buku yang Dipinjam");
            Console.WriteLine("7. Keluar");
            Console.Write("Pilih menu (1-7): ");

            string pilihan = Console.ReadLine();

            Console.Clear();

            switch (pilihan)
            {
                case "1":
                    Console.Write("ID Buku: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Judul: ");
                    string judul = Console.ReadLine();
                    Console.Write("Penulis: ");
                    string penulis = Console.ReadLine();
                    Console.Write("Tahun Terbit: ");
                    int tahun = int.Parse(Console.ReadLine());
                    perpustakaan.TambahBuku(new Book(id, judul, penulis, tahun));
                    break;

                case "2":
                    perpustakaan.TampilkanSemuaBuku();
                    break;

                case "3":
                    Console.Write("Masukkan ID Buku yang akan diupdate: ");
                    int idUpdate = int.Parse(Console.ReadLine());
                    perpustakaan.UpdateBuku(idUpdate);
                    break;

                case "4":
                    Console.Write("Masukkan ID Buku yang akan dihapus: ");
                    int idDelete = int.Parse(Console.ReadLine());
                    perpustakaan.HapusBuku(idDelete);
                    break;

                case "5":
                    perpustakaan.TampilkanSemuaBuku();
                    Console.Write("\nMasukkan ID buku yang ingin dipinjam: ");
                    int idPinjam = int.Parse(Console.ReadLine());
                    perpustakaan.PinjamBuku(idPinjam);
                    break;

                case "6":
                    perpustakaan.TampilkanBukuDipinjam();
                    Console.Write("\nMasukkan ID buku yang ingin dikembalikan: ");
                    int idKembali = int.Parse(Console.ReadLine());
                    perpustakaan.KembalikanBuku(idKembali);
                    break;

                case "7":
                    berjalan = false;
                    Console.WriteLine("Terimakasih.");
                    break;

                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }

            if (berjalan)
            {
                Console.WriteLine("\nTekan ENTER untuk kembali");
                Console.ReadLine();
            }
        }
    }
}

class Book
{
    public int ID { get; set; }
    public string Judul { get; set; }
    public string Penulis { get; set; }
    public int TahunTerbit { get; set; }

    public Book(int id, string judul, string penulis, int tahun)
    {
        ID = id;
        Judul = judul;
        Penulis = penulis;
        TahunTerbit = tahun;
    }
}

class PinjamBuku : Book
{
    public DateTime TanggalPinjam { get; set; }

    public PinjamBuku(Book buku) : base(buku.ID, buku.Judul, buku.Penulis, buku.TahunTerbit)
    {
        TanggalPinjam = DateTime.Now;
    }
}

class Library
{
    public string Nama { get; set; }
    private List<Book> koleksiBuku;
    private List<PinjamBuku> bukuDipinjam;

    public Library(string nama)
    {
        Nama = nama;
        koleksiBuku = new List<Book>();
        bukuDipinjam = new List<PinjamBuku>();
    }

    public void TambahBuku(Book buku)
    {
        koleksiBuku.Add(buku);
        Console.WriteLine("Buku berhasil ditambahkan.");
    }

    public void TampilkanSemuaBuku()
    {
        Console.WriteLine("\nDaftar Buku Tersedia:");
        foreach (var buku in koleksiBuku)
        {
            Console.WriteLine($"ID: {buku.ID}, Judul: {buku.Judul}, Penulis: {buku.Penulis}, Tahun: {buku.TahunTerbit}");
        }

        if (koleksiBuku.Count == 0)
        {
            Console.WriteLine("Tidak ada buku tersedia.");
        }
    }

    public void UpdateBuku(int id)
    {
        var buku = koleksiBuku.FirstOrDefault(buk => buk.ID == id);
        if (buku != null)
        {
            Console.Write("Judul: ");
            buku.Judul = Console.ReadLine();
            Console.Write("Penulis: ");
            buku.Penulis = Console.ReadLine();
            Console.Write("Tahun terbit: ");
            buku.TahunTerbit = int.Parse(Console.ReadLine());
            Console.WriteLine("Buku berhasil diupdate.");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }

    public void HapusBuku(int id)
    {
        var buku = koleksiBuku.FirstOrDefault(buk => buk.ID == id);
        if (buku != null)
        {
            koleksiBuku.Remove(buku);
            Console.WriteLine("Buku berhasil dihapus.");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan.");
        }
    }

    public void PinjamBuku(int id)
    {
        var buku = koleksiBuku.FirstOrDefault(buk => buk.ID == id);
        if (buku != null)
        {
            koleksiBuku.Remove(buku);
            bukuDipinjam.Add(new PinjamBuku(buku));
            Console.WriteLine($"Buku \"{buku.Judul}\" berhasil dipinjam.");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan atau sudah dipinjam.");
        }
    }

    public void TampilkanBukuDipinjam()
    {
        Console.WriteLine("\nDaftar Buku Dipinjam:");
        foreach (var buku in bukuDipinjam)
        {
            Console.WriteLine($"ID: {buku.ID}, Judul: {buku.Judul}, Penulis: {buku.Penulis}, Tanggal Pinjam: {buku.TanggalPinjam}");
        }

        if (bukuDipinjam.Count == 0)
        {
            Console.WriteLine("Tidak ada buku yang sedang dipinjam.");
        }
    }

    public void KembalikanBuku(int id)
    {
        var buku = bukuDipinjam.FirstOrDefault(b => b.ID == id);
        if (buku != null)
        {
            bukuDipinjam.Remove(buku);
            koleksiBuku.Add(new Book(buku.ID, buku.Judul, buku.Penulis, buku.TahunTerbit));
            Console.WriteLine($"Buku \"{buku.Judul}\" berhasil dikembalikan.");
        }
        else
        {
            Console.WriteLine("Buku tidak ditemukan dalam daftar pinjaman.");
        }
    }
}