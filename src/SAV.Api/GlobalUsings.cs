global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using System.Net;
global using System.Text.Json;
global using AutoMapper;

global using SAV.Core.Entities.Catalogo;
global using SAV.Core.Entities.Inventario;
global using SAV.Core.Entities.RedDeDistribuciónYPersonal;
global using SAV.Core.Entities.Seguridad;
global using SAV.Core.Entities.Ventas;
global using SAV.Core.Enums;

global using SAV.Application.Interfaces.Repositories;
global using SAV.Application.Interfaces.Services;
global using SAV.Application.Services;
global using SAV.Contracts.Dtos;
global using SAV.Contracts.Common;
global using SAV.Infrastructure.Persistence.Repositories;
global using SAV.Api.Responses;
global using SAV.Api.Authentication;
global using SAV.Api.Authorization;
global using SAV.Application.Common;