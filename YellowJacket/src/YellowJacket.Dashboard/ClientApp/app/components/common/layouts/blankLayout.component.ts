import { Component } from '@angular/core';

declare var jQuery: any;
import * as $ from 'jquery';

@Component({
    selector: 'blank',
    templateUrl: 'blankLayout.template.html'
})
export class BlankLayoutComponent {
    ngAfterViewInit() {
        $('body').addClass('gray-bg');
    }

    ngOnDestroy() {
        $('body').removeClass('gray-bg');
    }
}
