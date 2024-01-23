using ResturantManagement_Core.DTO;
using ResturantManagement_Core.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.Helper
{
    public static class MappingHelper
    {
       
        public static List<MenuDTO> MenuDtoMapper(List<Menu> Menus)
        {
            List<MenuDTO> MenuDtos = new List<MenuDTO>();
            foreach(Menu Menu in Menus)
            {
                MenuDTO dto = new MenuDTO();
                dto.MenuId = Menu.MenuId;
                dto.Name = Menu.Name;
                dto.Price = Menu.Price;
                MenuDtos.Add(dto);
            }
            return MenuDtos;
        }
    }
}
