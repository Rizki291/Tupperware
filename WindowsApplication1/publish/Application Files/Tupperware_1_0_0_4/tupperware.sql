-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Oct 16, 2019 at 12:02 PM
-- Server version: 10.4.6-MariaDB
-- PHP Version: 7.3.9

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
-- Table structure for table `barang`
--

CREATE TABLE `barang` (
  `kdbarang` varchar(20) NOT NULL,
  `nama` varchar(50) NOT NULL,
  `jenis` varchar(50) NOT NULL,
  `harga` double NOT NULL,
  `stok` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `barang`
--

INSERT INTO `barang` (`kdbarang`, `nama`, `jenis`, `harga`, `stok`) VALUES
('1', 'Box-355', 'Kotak Makan', 12000, 10),
('2', 'Barang Mahal', 'Rantang', 1000000, 78);

-- --------------------------------------------------------

--
-- Table structure for table `cicilan`
--

CREATE TABLE `cicilan` (
  `IDcicilan` int(11) NOT NULL,
  `Notrans` varchar(10) NOT NULL,
  `Date` date NOT NULL,
  `TotalBayar` int(11) NOT NULL,
  `CicilanSisa` int(11) NOT NULL,
  `Lunas` enum('Lunas','Belum Lunas') NOT NULL,
  `Nama` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `cicilan`
--

INSERT INTO `cicilan` (`IDcicilan`, `Notrans`, `Date`, `TotalBayar`, `CicilanSisa`, `Lunas`, `Nama`) VALUES
(5, '54583327', '2019-10-15', 2000000, 1800000, 'Belum Lunas', 'mem-290');

-- --------------------------------------------------------

--
-- Table structure for table `member`
--

CREATE TABLE `member` (
  `nokartu` varchar(20) NOT NULL,
  `nama` varchar(50) NOT NULL,
  `alamat` varchar(120) NOT NULL,
  `noHp` varchar(13) NOT NULL,
  `lvl_member` varchar(20) NOT NULL,
  `poin_member` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `member`
--

INSERT INTO `member` (`nokartu`, `nama`, `alamat`, `noHp`, `lvl_member`, `poin_member`) VALUES
('0', 'Non-Member', '-', '0', '-', 0),
('mem-290', 'Noval', 'Jempong', '082154531735', '21318637126', 640);

-- --------------------------------------------------------

--
-- Table structure for table `penjualan`
--

CREATE TABLE `penjualan` (
  `no_transaksi` varchar(10) NOT NULL,
  `nokartu` varchar(20) NOT NULL,
  `tanggal` date NOT NULL,
  `Total_harga` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `penjualan`
--

INSERT INTO `penjualan` (`no_transaksi`, `nokartu`, `tanggal`, `Total_harga`) VALUES
('40939424', '0', '2019-10-15', 2000000),
('54583327', '0', '2019-10-15', 2000000),
('56837198', 'mem-290', '2019-10-16', 1000000),
('64354337', 'mem-290', '2019-10-15', 2000000),
('97857088', '0', '2019-10-15', 2000000);

-- --------------------------------------------------------

--
-- Table structure for table `penjualan_detail`
--

CREATE TABLE `penjualan_detail` (
  `ID_dtl` int(11) NOT NULL,
  `kdbarang` varchar(20) NOT NULL,
  `qty` int(11) NOT NULL,
  `total_harga` int(11) NOT NULL,
  `Id_transaksi` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `penjualan_detail`
--

INSERT INTO `penjualan_detail` (`ID_dtl`, `kdbarang`, `qty`, `total_harga`, `Id_transaksi`) VALUES
(17, '2', 2, 2000000, '64354337'),
(18, '2', 2, 2000000, '97857088'),
(19, '2', 2, 2000000, '54583327'),
(20, '2', 2, 2000000, '40939424'),
(21, '2', 1, 1000000, '56837198');

--
-- Triggers `penjualan_detail`
--
DELIMITER $$
CREATE TRIGGER `Tambah_stok` AFTER INSERT ON `penjualan_detail` FOR EACH ROW BEGIN
	UPDATE barang,penjualan_detail SET barang.stok=barang.stok-penjualan_detail.qty
    WHERE barang.kdbarang=penjualan_detail.kdbarang;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id_user` int(3) NOT NULL,
  `username` varchar(10) NOT NULL,
  `password` varchar(8) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id_user`, `username`, `password`) VALUES
(1, 'Admin', 'Admin');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`kdbarang`);

--
-- Indexes for table `cicilan`
--
ALTER TABLE `cicilan`
  ADD PRIMARY KEY (`IDcicilan`),
  ADD KEY `Cicilantrans` (`Notrans`);

--
-- Indexes for table `member`
--
ALTER TABLE `member`
  ADD PRIMARY KEY (`nokartu`);

--
-- Indexes for table `penjualan`
--
ALTER TABLE `penjualan`
  ADD PRIMARY KEY (`no_transaksi`),
  ADD KEY `nokartu` (`nokartu`);

--
-- Indexes for table `penjualan_detail`
--
ALTER TABLE `penjualan_detail`
  ADD PRIMARY KEY (`ID_dtl`),
  ADD KEY `Id_transaksi` (`Id_transaksi`),
  ADD KEY `kdbarang` (`kdbarang`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id_user`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cicilan`
--
ALTER TABLE `cicilan`
  MODIFY `IDcicilan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `penjualan_detail`
--
ALTER TABLE `penjualan_detail`
  MODIFY `ID_dtl` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cicilan`
--
ALTER TABLE `cicilan`
  ADD CONSTRAINT `Cicilantrans` FOREIGN KEY (`Notrans`) REFERENCES `penjualan` (`no_transaksi`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `penjualan`
--
ALTER TABLE `penjualan`
  ADD CONSTRAINT `penjualan_ibfk_2` FOREIGN KEY (`nokartu`) REFERENCES `member` (`nokartu`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `penjualan_detail`
--
ALTER TABLE `penjualan_detail`
  ADD CONSTRAINT `penjualan_detail_ibfk_1` FOREIGN KEY (`Id_transaksi`) REFERENCES `penjualan` (`no_transaksi`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `penjualan_detail_ibfk_2` FOREIGN KEY (`kdbarang`) REFERENCES `barang` (`kdbarang`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
