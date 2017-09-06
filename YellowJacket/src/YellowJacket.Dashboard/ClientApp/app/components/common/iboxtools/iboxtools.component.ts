import { Component } from '@angular/core';

declare var jQuery: any;
import * as $ from 'jquery';

@Component({
    selector: 'iboxtools',
    templateUrl: 'iboxtools.template.html'
})

export class IboxtoolsComponent {
    collapse(e: any): void {
        e.preventDefault();

        const ibox = $(e.target).closest('div.ibox');
        const button = $(e.target).closest('i');
        const content = ibox.children('.ibox-content');

        content.slideToggle(200);
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        ibox.toggleClass('').toggleClass('border-bottom');
        setTimeout(() => {
            ibox.resize();
            ibox.find('[id^=map-]').resize();
        }, 50);

    }

    close(e: any): void {
        e.preventDefault();

        const content = $(e.target).closest('div.ibox');

        content.remove();
    }
}
