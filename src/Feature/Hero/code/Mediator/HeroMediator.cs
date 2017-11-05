﻿using System.Web;
using Glass.Mapper.Sc;
using Helixbase.Feature.Hero.Services;
using Helixbase.Feature.Hero.ViewModels;
using Helixbase.Foundation.Models.Mediators;

namespace Helixbase.Feature.Hero.Mediator
{
    public class HeroMediator : MediatorBase, IHeroMediator
    {
        private readonly IGlassHtml _glassHtml;
        private readonly IHeroService _heroService;

        public HeroMediator(IGlassHtml glassHtml, IHeroService heroService)
        {
            _glassHtml = glassHtml;
            _heroService = heroService;
        }

        public MediatorResponse<HeroViewModel> CreateHeroViewModel()
        {
            var heroItemDataSource = _heroService.GetHeroItems();

            if (heroItemDataSource == null)
                return GetMediatorResponse<HeroViewModel>(MediatorCodes.HeroResponse.DataSourceError);

            var viewModel = new HeroViewModel
            {
                HeroImages = heroItemDataSource.HeroImages,
                HeroTitle = new HtmlString(_glassHtml.Editable(heroItemDataSource, i => i.HeroTitle,
                    new {EnclosingTag = "h2"})),
                IsExperienceEditor = _heroService.IsExperienceEditor
            };

            return GetMediatorResponse(MediatorCodes.HeroResponse.Ok, viewModel);
        }
    }
}