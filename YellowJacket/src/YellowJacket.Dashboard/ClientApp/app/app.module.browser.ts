import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';

// App modules/components
import { LayoutsModule } from './components/common/layouts/layouts.module';


@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared,
        LayoutsModule
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
