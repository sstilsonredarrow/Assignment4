﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assignment4Core.Models;
using MvvmCross.ViewModels;
using static Assignment4Core.Models.MapDirections;

namespace Assignment4Core.ViewModels
{
    public class DirectionsViewModel : MvxViewModel<RootDirections>
    {
        private RootDirections _rootDirections;

        public List<Step> Steps
        {
            get { return _rootDirections.routes[0].legs.SelectMany(l => l.steps).ToList(); }
        }

        public DirectionsViewModel()
        {
        }

        public override void Prepare(RootDirections parameter)
        {
            base.Prepare();

            parameter.routes[0].legs.ForEach(l => {
                var steps = l.steps;
                steps.ForEach(s =>
                {
                    var helper = new Helpers.HTMLStripper();
                    var result = helper.RemoveHTML(s.html_instructions);
                    s.html_instructions = result;
                });
            });

            _rootDirections = parameter;
            RaiseAllPropertiesChanged();
        }
    }
}
