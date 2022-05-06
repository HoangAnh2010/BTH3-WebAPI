USE BTH3
CREATE TABLE NhanVien(
 MaNV varchar(30) PRIMARY KEY,
 HoTen nvarchar(30) not null,
 NgaySinh date,
 GioiTinh nvarchar(3),
 DiaChi nvarchar(max),
 MaPB varchar(30),
 CONSTRAINT FR_nv_phongban FOREIGN KEY (MaPB) REFERENCES
PhongBan(MaPB)
)
CREATE TABLE Phongban(
MaPB varchar(30) PRIMARY KEY,
TenPB nvarchar(100),
)
CREATE TABLE Congtrinh(
	MaCT varchar(30) PRIMARY KEY,
	TenCT nvarchar(50) NOT NULL,
	DiaDiem nvarchar(Max),
	NGAYCAPGP date,
	NGAYKC date	
)
--drop table congtrinh
CREATE TABLE Cong(
MaCT varchar(30) ,
MaNV varchar(30) ,
SLNgayCong int,
primary key (MaCT,MaNV)
)

insert into PhongBan(mapb,tenpb)
values('p1','IT'),('p2',N'Nhân sự'),('p3',N'Bảo vệ')
--delete from phongban

INSERT INTO NHANVIEN(MaNV,HoTen,NgaySinh,GioiTinh,DiaChi,MaPB)
VALUES
('nv1',N'Trần Thị Thanh Nhàn','2001-02-25',N'Nữ',N'Khoái Châu','p1'),
('nv2',N'Phạm Thị Thu Trang','2001-01-20',N'Nữ',N'Phù Cừ','p2'),
('nv3',N'Vũ Thị Bích Loan','2001-01-02',N'Nữ',N'Hồng Tiến','p3'),
('nv04',N'Bùi Thị Hồng Hạnh','05-10-1991',N'Nữ',N'Tiên Lữ','p1'),
('nv05',N'Nguyễn Hoàng Anh','10-20-1997',N'Nữ',N'Phù Cừ','p1'),
('nv06',N'Phạm Thị Thảo Ly','05-17-1989',N'Nữ',N'Khoái Châu','p1')

INSERT INTO Congtrinh (MaCT, TenCT, DiaDiem, NGAYCAPGP, NGAYKC) 
VALUES ('CT1', N'Nhà cấp 4', 'HaNoi', '03/15/2020', '03/15/2022'),
('CT2', N'Hạ tầng', 'HaNoi', '03/10/2020', '03/10/2022'),
('CT3', N'Tòa nhà 25 tầng', 'HaiPhong', '02/12/2020', '02/12/2022'),
('CT4', N'Chung cư cao cấp', 'HungYen','01/22/2020', '01/22/2022')

insert into cong(manv,mact,SLNgayCong)
values ('nv1','ct1','25'),
		('nv2','ct2','20'),
		('nv3','ct3','25'),
		('nv5','ct4','28')
---them nv
alter PROC ThemNV (@manv varchar(30),@hoten nvarchar(30),
					@ngaysinh date, @gioitinh nvarchar(3),
					@diachi nvarchar(max), @mapb varchar(30))
As
BEGIN
if(exists (select manv from Nhanvien where manv=@manv ))
begin
	print N'Mã nv '+@manv+N' đã tồn tại' 
	return -1
end
INSERT INTO NhanVien(Manv, hoten,ngaysinh,gioitinh,diachi,mapb)
VALUES (@manv ,@hoten,@ngaysinh, @gioitinh ,@diachi , @mapb)
return 0
END;
exec themnv 'nv05','string','10/10/2001','string','string','p1'
----suanv
CREATE PROC SuaNV (@manv varchar(30),@hoten nvarchar(30),
					@ngaysinh date, @gioitinh nvarchar(3),
					@diachi nvarchar(max), @mapb varchar(30))
As
BEGIN
UPDATE NhanVien SET hoten=@hoten,NgaySinh=@ngaysinh,GioiTinh=@gioitinh,DiaChi=@diachi,MaPB=@mapb
WHERE MaNV=@manv
END;

----xoanv
CREATE PROC XoaNV (@manv varchar(30) )
As
BEGIN
DELETE FROM NhanVien
WHERE MaNV=@manv
END;

---get nv theo ma
CREATE FUNCTION F_NhanVienTheoMa(@ma varchar(30))
RETURNS TABLE
AS
  RETURN (SELECT * FROM NhanVien WHERE MaNV=@ma)
--Thực thi
SELECT*FROM F_NhanVienTheoMa('NV1')

---get nv theo tuoi
create function f_nvtheotuoi(@a int, @b int)
returns table
as
return
	(select *
	from NHANVIEN 
	where (DATEPART(yy,GETDATE()) - DATEPART(yy,NHANVIEN.NgaySinh))>@a and (DATEPART(yy,GETDATE())- DATEPART(yy,NHANVIEN.NgaySinh)) < @b)
--gọi hàm
select*from f_nvtheotuoi(19, 25)

---dem so nv theo gioi tinh
create FUNCTION F_SoNVTheoGT (@gt nvarchar(3))
returns int
AS
begin
	declare @sl int;
	select @sl= count(*) from NhanVien where GioiTinh = @gt
RETURN @sl
end;
---get theo ten
CREATE FUNCTION F_NhanVienTheoTen(@hoten nvarchar(30))
RETURNS TABLE
AS
  RETURN (SELECT * FROM NhanVien WHERE HoTen like '%'+@hoten+'%')
--Thực thi
SELECT*FROM F_NhanVienTheoTen(N'Nguyễn Hoàng Anh')

---get theo dia chi
CREATE FUNCTION F_NhanVienTheoDC(@diachi nvarchar(max))
RETURNS TABLE
AS
  RETURN (SELECT * FROM NhanVien WHERE DiaChi like '%'+@diachi+'%')
--Thực thi
SELECT*FROM F_NhanVienTheoDC(N'Phù Cừ')

---them pb
create PROC ThemPB (@mapb varchar(30),@tenpb nvarchar(100))
As
BEGIN
if(exists (select mapb from Phongban where MaPB=@mapb ))
begin
	print N'Mã PB'+@mapb+N' đã tồn tại' 
	return -1
end
INSERT INTO Phongban(MaPB, TenPB)
VALUES (@mapb ,@tenpb)
return 0
END;

---sua pb
CREATE PROC SuaPB (@mapb varchar(30),@tenpb nvarchar(100))
As
BEGIN
UPDATE Phongban SET TenPB=@tenpb
WHERE MaPB=@mapb
END;

---xoapb
CREATE PROC XoaPB (@mapb varchar(30) )
As
BEGIN
DELETE FROM Phongban
WHERE MaPB=@mapb
END;

---themct
create PROC ThemCongTrinh (@mact varchar(30),@tenct nvarchar(50),
						@diadiem nvarchar(max), @ngaycapgp date, @ngaykc date)
As
BEGIN
if(exists (select mact from Congtrinh where mact=@mact ))
begin
	print N'Mã CT'+@mact+N' đã tồn tại' 
	return -1
end
INSERT INTO Congtrinh(Mact,TenCT,DiaDiem,NGAYCAPGP,NGAYKC)
VALUES (@mact ,@tenct,@diadiem,@ngaycapgp,@ngaykc)
return 0
END;

----suact
CREATE PROC SuaCT (@mact varchar(30),@tenct nvarchar(50),
						@diadiem nvarchar(max), @ngaycapgp date, @ngaykc date)
As
BEGIN
UPDATE Congtrinh SET TenCT=@tenct,DiaDiem=@diadiem,NGAYCAPGP=@ngaycapgp,NGAYKC=@ngaykc
WHERE MaCT=@mact
END;

----xoact
CREATE PROC XoaCT (@mact varchar(30) )
As
BEGIN
DELETE FROM Congtrinh
WHERE MaCT=@mact
END;

---them cong
create proc ThemCong(@mact varchar(30), @manv varchar(30), @slngaycong int)
as
begin
if(exists (select mact,manv from Cong where manv=@manv and mact=@mact ))
begin
	print N'Mã Cong'+@mact+N' đã tồn tại' 
	return -1
end
insert into Cong(MaCT,MaNV,SLNgayCong)
values(@mact,@manv,@slngaycong)
return 0
end

----sua cong
CREATE PROC SuaCong (@mact varchar(30),@manv varchar(30),@slngaycong int)
As
BEGIN
UPDATE Cong SET SLNgayCong=@slngaycong
WHERE MaCT=@mact and MaNV=@manv
END;

----xoa cong
CREATE PROC XoaCong (@mact varchar(30),@manv varchar(30) )
As
BEGIN
DELETE FROM Cong
WHERE MaCT=@mact and MaNV=@manv
END;