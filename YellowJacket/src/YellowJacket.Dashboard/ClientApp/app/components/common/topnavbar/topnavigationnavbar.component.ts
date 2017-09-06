import { Component } from '@angular/core';
import {Router} from '@angular/router';
import { smoothlyMenu } from '../../../app.helpers';

//declare var jQuery:any;
import * as $ from 'jquery';

@Component({
  selector: 'topnavigationnavbar',
  templateUrl: 'topnavigationnavbar.template.html'
})
export class TopNavigationNavbarComponent {

  toggleNavigation(): void {
    $("body").toggleClass("mini-navbar");
    smoothlyMenu();
  }

}
