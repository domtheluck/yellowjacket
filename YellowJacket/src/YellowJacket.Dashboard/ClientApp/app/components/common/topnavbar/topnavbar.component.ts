import { Component } from '@angular/core';
import { smoothlyMenu } from '../../../app.helpers';

declare var jQuery: any;
import * as $ from 'jquery';

@Component({
    selector: 'topnavbar',
    templateUrl: 'topnavbar.template.html'
})

export class TopNavbarComponent {
    toggleNavigation(): void {
        $('body').toggleClass('mini-navbar');
        smoothlyMenu();
    }
}
