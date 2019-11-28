-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 02 Sep 2019 pada 13.49
-- Versi server: 10.4.6-MariaDB
-- Versi PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tupperware`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `barang`
--

CREATE TABLE `barang` (
  `kdbarang` varchar(20) NOT NULL,
  `nama` varchar(50) NOT NULL,
  `jenis` varchar(50) NOT NULL,
  `harga` double NOT NULL,
  `stok` int(3) NOT NULL,
  `poin_barang` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `barang`
--

INSERT INTO `barang` (`kdbarang`, `nama`, `jenis`, `harga`, `stok`, `poin_barang`) VALUES
('123', 'babi', 'sempak', 1000, 1, 1);

-- --------------------------------------------------------

--
-- Struktur dari tabel `member`
--

CREATE TABLE `member` (
  `nokartu` varchar(20) NOT NULL,
  `nama` varchar(50) NOT NULL,
  `alamat` varchar(120) NOT NULL,
  `noHp` int(13) NOT NULL,
  `lvl_member` varchar(20) NOT NULL,
  `poin_member` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `member`
--

INSERT INTO `member` (`nokartu`, `nama`, `alamat`, `noHp`, `lvl_member`, `poin_member`) VALUES
('0008', 'noval-chan', 'pusuck', 812345678, '2', 24);

-- --------------------------------------------------------

--
-- Struktur dari tabel `penjualan`
--

CREATE TABLE `penjualan` (
  `id_jual` int(5) NOT NULL,
  `no_transaksi` varchar(10) NOT NULL,
  `kdbarang` varchar(20) NOT NULL,
  `nokartu` varchar(20) NOT NULL,
  `tanggal` date NOT NULL,
  `qty` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `penjualan`
--

INSERT INTO `penjualan` (`id_jual`, `no_transaksi`, `kdbarang`, `nokartu`, `tanggal`, `qty`) VALUES
(1, 'ID1312', '123', '0008', '2019-09-02', 2),
(2, '2', '123', '0008', '2019-09-02', 1);

--
-- Trigger `penjualan`
--
DELIMITER $$
CREATE TRIGGER `kurangStok` AFTER INSERT ON `penjualan` FOR EACH ROW BEGIN
	UPDATE barang,penjualan SET barang.stok=barang.stok-penjualan.qty
    WHERE barang.kdbarang=penjualan.kdbarang;
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `tambahPoin` AFTER INSERT ON `penjualan` FOR EACH ROW BEGIN
	UPDATE member a, barang b, penjualan c SET a.poin_member=a.poin_member+b.poin_barang
    WHERE c.nokartu=c.nokartu AND b.kdbarang=c.kdbarang;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Struktur dari tabel `user`
--

CREATE TABLE `user` (
  `id_user` int(3) NOT NULL,
  `username` varchar(10) NOT NULL,
  `password` varchar(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `user`
--

INSERT INTO `user` (`id_user`, `username`, `password`) VALUES
(1, 'a', 'b');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`kdbarang`);

--
-- Indeks untuk tabel `member`
--
ALTER TABLE `member`
  ADD PRIMARY KEY (`nokartu`);

--
-- Indeks untuk tabel `penjualan`
--
ALTER TABLE `penjualan`
  ADD PRIMARY KEY (`id_jual`),
  ADD KEY `id` (`id_jual`),
  ADD KEY `kdbarang` (`kdbarang`),
  ADD KEY `nokartu` (`nokartu`);

--
-- Indeks untuk tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `penjualan`
--
ALTER TABLE `penjualan`
  MODIFY `id_jual` int(5) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `penjualan`
--
ALTER TABLE `penjualan`
  ADD CONSTRAINT `penjualan_ibfk_1` FOREIGN KEY (`kdbarang`) REFERENCES `barang` (`kdbarang`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `penjualan_ibfk_2` FOREIGN KEY (`nokartu`) REFERENCES `member` (`nokartu`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
