using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalRiwi.Migrations
{
    /// <inheritdoc />
    public partial class InitialModelSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "speciality",
                table: "Doctors",
                newName: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Patients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "Appointment",
                table: "MedicalDates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "MedicalDates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "MedicalDates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MedicalDates",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDates_DoctorId",
                table: "MedicalDates",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDates_PatientId",
                table: "MedicalDates",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDates_Doctors_DoctorId",
                table: "MedicalDates",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDates_Patients_PatientId",
                table: "MedicalDates",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDates_Doctors_DoctorId",
                table: "MedicalDates");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDates_Patients_PatientId",
                table: "MedicalDates");

            migrationBuilder.DropIndex(
                name: "IX_MedicalDates_DoctorId",
                table: "MedicalDates");

            migrationBuilder.DropIndex(
                name: "IX_MedicalDates_PatientId",
                table: "MedicalDates");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Appointment",
                table: "MedicalDates");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "MedicalDates");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "MedicalDates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MedicalDates");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Doctors",
                newName: "speciality");
        }
    }
}
