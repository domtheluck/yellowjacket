import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';

import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';

// App modules/components
import { LayoutsModule } from './components/common/layouts/layouts.module';

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        ServerModule,
        AppModuleShared,
        LayoutsModule
    ]
})
export class AppModule {
}
