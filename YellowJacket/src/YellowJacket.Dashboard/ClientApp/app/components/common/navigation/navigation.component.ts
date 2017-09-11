import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'navigation',
    templateUrl: 'navigation.template.html'
})

export class NavigationComponent {
    constructor(private readonly router: Router) { }

    public activeRoute(routename: string): boolean {
        return this.router.url.indexOf(routename) > -1;
    }
}
