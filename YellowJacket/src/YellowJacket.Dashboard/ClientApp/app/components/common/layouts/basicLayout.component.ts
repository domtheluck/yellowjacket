import 'font-awesome/css/font-awesome.css';

import { Component } from '@angular/core';
import { detectBody } from '../../../app.helpers';

declare var jQuery: any;
import * as $ from 'jquery';

@Component({
    selector: 'basic',
    templateUrl: 'basicLayout.template.html',
    host: {
        '(window:resize)': 'onResize()'
    }
})

export class BasicLayoutComponent {
    public ngOnInit(): any {
        detectBody();
    }

    public onResize() {
        detectBody();
    }
}
