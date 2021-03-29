using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cs.Application.Interfaces;
using Cs.Application.Org.Models;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Cs.Application.Org
{
    public class OrganizationService : ICrudOperation<OrganizationViewModel>
    {
        private readonly IAuthDbService _db;
        private readonly IMapper _mapper;

        public OrganizationService(IAuthDbService db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ListResult<OrganizationViewModel> Get(Paging paging)
        {
            var orgs = _db.Organizations.All();
            if (!string.IsNullOrEmpty(paging.Search))
                orgs = orgs.Where(t => t.Name.Contains(paging.Search)
                                       || t.TaxCode.Contains(paging.Search)
                                       || t.PhoneNumber.Contains(paging.Search));

            var result = orgs.Include(o => o.Roles).Skip(paging.Skip).Take(paging.Count).ToList();

            var orgViewModels = _mapper.Map<List<OrganizationViewModel>>(result);

            var total = orgs.Count();

            return new ListResult<OrganizationViewModel>(orgViewModels, total);
        }

        public OrganizationViewModel Get(int id)
        {
            var organization = _db.Organizations.All().Include(org => org.Roles).FirstOrDefault(org => org.Id == id);
            if(organization == null)
                throw new ServiceException(UiConstants.ObjectNotFound);
            
            return _mapper.Map<OrganizationViewModel>(organization);
        }

        public int Add(OrganizationViewModel orgViewModel)
        {
            if (_db.Organizations.All().Any(bc => bc.Name == orgViewModel.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(orgViewModel.Name));

            var orgToCreate = _mapper.Map<Organization>(orgViewModel);

            _db.Organizations.Add(orgToCreate);
            _db.Organizations.Save();

            return orgToCreate.Id;
        }

        public void Update(OrganizationViewModel orgViewModel)
        {
            if (_db.Organizations.All().Any(bc => bc.Name == orgViewModel.Name && bc.Id != orgViewModel.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(orgViewModel.Name));

            var orgToUpdate = _db.Organizations.All().Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == orgViewModel.Id);

            if (orgToUpdate != null)
            {
                _mapper.Map(orgViewModel, orgToUpdate);

                _db.Organizations.Update(orgToUpdate);
                _db.Organizations.Save();
            }
        }

        public void Delete(int id)
        {
            _db.Organizations.Delete(id);
            _db.Organizations.Save();
        }
    }
}