
using WebAPI.Models.DTOs;

namespace Shared.Interface;

public interface IHomeApi
{
    IActionResult GetForms();
    IActionResult GetFormById(Guid id);
    IActionResult CreateForm(FormeDto formeDto);
    IActionResult UpdateForm(Guid id, FormeDto formeDto);
    IActionResult DeleteForm(Guid id);
}