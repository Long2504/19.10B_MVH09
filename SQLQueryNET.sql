create database NET2
use NET2
create table may
(
MaMay nchar(5) 
constraint pk_may_MaMay primary key ,
TenMay nvarchar(100),
TrangThai bit ,
Phong int NOT NULL,
IDTK nchar(5) unique ,
IPcLient char(20)
)
create table TaiKhoan
(
IDTK nchar(5)
constraint pk_TaiKhoan_IDTK primary key ,
TenTK nchar(30) NOT NULL unique,
MK varchar(30) NOT NULL,
SoDu int,
IDKH nchar(5)
)

create table KhachHang
(
IDKH nchar(5) 
constraint pk_KhachHang_IDKH primary key ,
TenKH nvarchar (40) NOT NULL,
CMND int NOT NULL,
SdtKH nchar(10),
)
create table Phong(
IDPhong int primary key NOT NULL,
TenPhong nvarchar(100) not null,
GiaPhong int Not Null
)
create table DichVuKH(
MaDV int primary key,
TenDV nvarchar(30),
)
create table NhanVien
(
MaNV nchar (5)
constraint pk_NhanVien_MaNV primary key,
TenNV nvarchar(40) NOT NULL,
Luong int,
GioiTinh bit,
Tinhtrang bit default 1,
DiaChi nvarchar(40) NOT NULL,
CMND nchar(10),
sdtnv nchar(10) ,
)
create table bill
(
IDBill nchar(5) 
constraint pk_bill_Mabill primary key,
MaNV nchar(5),
MaDV int ,
IDTK nchar(5),
IDOrder nchar(5),
TongTien INT,
)

create Table Food(
IDFood nchar(5) constraint pk_F primary key,
TenFood nvarchar(30) not null,
Gia int,
TypeF nchar(5),
)

create Table TypeFood(
IDType nchar(5) constraint pk_TFD primary key,
TenType nvarchar(30) not null
)

create Table ListFoodOrder(
id int identity constraint PK_LOR primary key,
IDOrder nchar(5),
IDFood nchar(5),
Soluong int,
TongTien int,
)
create table KHorder(
IDOrder nchar(5) constraint pk_Or primary key,
IDMay nchar(5),
)
alter table Food add constraint fk_Food_TypeF Foreign key (TypeF) references TypeFood(IDType) on delete cascade on update cascade
alter table ListFoodOrder add constraint fk_LOR_F Foreign key (IDFood) references Food(IDFood) on delete cascade on update cascade
alter table ListFoodOrder add constraint fk_LOR_Or Foreign key (IDOrder) references KHorder(IDOrder) on delete cascade on update cascade
alter table KHOrder add constraint fk_OR_M Foreign key (IDMay) references May(MaMay) on delete cascade on update cascade
alter table bill add constraint fk_B_Or Foreign key (IDOrder) references KHOrder(IDOrder) on delete cascade on update cascade
alter table bill add constraint fk_B_NV Foreign key (MaNV) references NhanVien(MaNV) on delete cascade on update cascade
alter table bill add constraint fk_B_DV Foreign key (MaDV) references DichVuKH(MaDV) on delete cascade on update cascade
alter table bill add constraint fk_B_TK Foreign key (IDTK) references TaiKhoan(IDTK)

alter table TaiKhoan add
	constraint pk_TaiKkhoan_KhachHang Foreign key (IDKH)
	references KhachHang(IDKH) 
	on delete cascade on update cascade
alter table may add
	constraint pk_may_TaiKhoan Foreign key (IDTK)
	references TaiKhoan(IDTK)
	on delete cascade on update cascade ,
	constraint pk_may_Phong Foreign key (Phong)
	references Phong(IDPhong)
	on delete cascade on update cascade

insert into DichVuKH(MaDV,TenDV) values (1,'Dangnhap-momay')
insert into DichVuKH(MaDV,TenDV) values (2,'dangxuat-dongmay')
insert into DichVuKH(MaDV,TenDV) values (3,'goi do an')
insert into DichVuKH(MaDV,TenDV) values (4,'nap tien')
