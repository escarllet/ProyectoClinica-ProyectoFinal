﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class addcampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horarios_Personas_DoctorId",
                table: "Horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Provincias_ProvinciaId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacaciones_Personas_EmployeeId",
                table: "Vacaciones");

            migrationBuilder.DropColumn(
                name: "EstaAprobada",
                table: "Vacaciones");

            migrationBuilder.DropColumn(
                name: "IdEmployee",
                table: "Vacaciones");

            migrationBuilder.DropColumn(
                name: "IdProvincia",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "IdDoctor",
                table: "Horarios");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioModificacion",
                table: "Vacaciones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioCreacion",
                table: "Vacaciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Vacaciones",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Vacaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Vacaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioModificacion",
                table: "Sustituciones",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioCreacion",
                table: "Sustituciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdDoctorSustituto",
                table: "Sustituciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Sustituciones",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "DoctorInterinoId",
                table: "Sustituciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorTitularId",
                table: "Sustituciones",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioModificacion",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioCreacion",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Provincias",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Provincias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinciaId",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioModificacion",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioCreacion",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Personas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "IdDoctor",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Personas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioModificacion",
                table: "Horarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioCreacion",
                table: "Horarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Horarios",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Horarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioModificacion",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuarioCreacion",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Sustituciones_DoctorInterinoId",
                table: "Sustituciones",
                column: "DoctorInterinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sustituciones_DoctorTitularId",
                table: "Sustituciones",
                column: "DoctorTitularId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UserId",
                table: "Personas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horarios_Personas_DoctorId",
                table: "Horarios",
                column: "DoctorId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_AspNetUsers_UserId",
                table: "Personas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Provincias_ProvinciaId",
                table: "Personas",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sustituciones_Personas_DoctorInterinoId",
                table: "Sustituciones",
                column: "DoctorInterinoId",
                principalTable: "Personas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sustituciones_Personas_DoctorTitularId",
                table: "Sustituciones",
                column: "DoctorTitularId",
                principalTable: "Personas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacaciones_Personas_EmployeeId",
                table: "Vacaciones",
                column: "EmployeeId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horarios_Personas_DoctorId",
                table: "Horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_AspNetUsers_UserId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Provincias_ProvinciaId",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_Sustituciones_Personas_DoctorInterinoId",
                table: "Sustituciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Sustituciones_Personas_DoctorTitularId",
                table: "Sustituciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacaciones_Personas_EmployeeId",
                table: "Vacaciones");

            migrationBuilder.DropIndex(
                name: "IX_Sustituciones_DoctorInterinoId",
                table: "Sustituciones");

            migrationBuilder.DropIndex(
                name: "IX_Sustituciones_DoctorTitularId",
                table: "Sustituciones");

            migrationBuilder.DropIndex(
                name: "IX_Personas_UserId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Vacaciones");

            migrationBuilder.DropColumn(
                name: "DoctorInterinoId",
                table: "Sustituciones");

            migrationBuilder.DropColumn(
                name: "DoctorTitularId",
                table: "Sustituciones");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Provincias");

            migrationBuilder.DropColumn(
                name: "IdDoctor",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Personas");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioModificacion",
                table: "Vacaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioCreacion",
                table: "Vacaciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Vacaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Vacaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "EstaAprobada",
                table: "Vacaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdEmployee",
                table: "Vacaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioModificacion",
                table: "Sustituciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioCreacion",
                table: "Sustituciones",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "IdDoctorSustituto",
                table: "Sustituciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Sustituciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioModificacion",
                table: "Provincias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioCreacion",
                table: "Provincias",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Provincias",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProvinciaId",
                table: "Personas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioModificacion",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioCreacion",
                table: "Personas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Personas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdProvincia",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioModificacion",
                table: "Horarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioCreacion",
                table: "Horarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaModificacion",
                table: "Horarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Horarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdDoctor",
                table: "Horarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioModificacion",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuarioCreacion",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Horarios_Personas_DoctorId",
                table: "Horarios",
                column: "DoctorId",
                principalTable: "Personas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Provincias_ProvinciaId",
                table: "Personas",
                column: "ProvinciaId",
                principalTable: "Provincias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacaciones_Personas_EmployeeId",
                table: "Vacaciones",
                column: "EmployeeId",
                principalTable: "Personas",
                principalColumn: "Id");
        }
    }
}
