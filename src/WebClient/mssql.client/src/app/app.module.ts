import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PrismModule } from '@ngx-prism/core'; // <----- Here
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { AppComponent } from './app.component'; 
import { AmexioDataModule } from 'amexio-ng-extensions';
import { AmexioWidgetModule } from 'amexio-ng-extensions';
import { NgxUiLoaderModule, NgxUiLoaderHttpModule, NgxUiLoaderConfig, POSITION, SPINNER, PB_DIRECTION } from 'ngx-ui-loader';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalModule, BsModalRef } from 'ngx-bootstrap/modal';
import { AmexioChartsModule } from 'amexio-ng-extensions';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AmexioEnterpriseModule } from 'amexio-ng-extensions';
import { AngularSplitModule } from 'angular-split';
import { TreeDragDropService } from 'primeng/api';
import { MessageService } from 'primeng/api';
import { AccordionModule } from 'primeng/accordion';
import { TableModule } from 'primeng/table';
import { ProgressBarModule } from "angular-progress-bar"
import { CommonModule } from '@angular/common';
import { TreeModule } from 'primeng/tree';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { ContextMenuModule } from 'primeng/contextmenu';
import { TabViewModule } from 'primeng/tabview';
import { CodeHighlighterModule } from 'primeng/codehighlighter'; 
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';

//import { AppRoutingModule } from './app-routing.module';
 
import { Routes, RouterModule } from '@angular/router';





import { MainPageComponent } from './ui/main-page/main-page.component'; 
import { HeaderComponent } from './ui/header/header.component';
import { FooterComponent } from './ui/footer/footer.component';

 
import { TablesModule } from './table/tables.module';
import { DatabaseModule } from './database/database.module'; 
import { LeftmenuComponent } from './left-menu/components/leftmenu/leftmenu.component';
import { ProcedureModule } from './procedure/procedure.module';
import { ProcedureComponent } from './procedure/components/procedure/procedure.component';
import { ProcedureService } from './procedure/service/procedure.service';


const appRoutes: Routes = [

   
];
const ngxUiLoaderConfig: NgxUiLoaderConfig = {

  "bgsColor": "#1c749a",
  "bgsOpacity": 0.7,
  "bgsPosition": "center-center",
  "bgsSize": 80,
  "bgsType": "ball-spin-clockwise",
  "blur": 5,
  "delay": 0,
  "fgsColor": "#1c749a",
  "fgsPosition": "center-center",
  "fgsSize": 60,
  "fgsType": "ball-spin-clockwise",
  "gap": 10,
  "logoPosition": "center-center",
  "logoSize": 120,
  "logoUrl": "",
  "masterLoaderId": "master",
  "overlayBorderRadius": "0",
  "overlayColor": "rgba(40, 40, 40, 0.8)",
  "pbColor": "red",
  "pbDirection": "ltr",
  "pbThickness": 1,
  "hasProgressBar": true,
  "text": "Loading",
  "textColor": "#FFFFFF",
  "textPosition": "center-center",
  "maxTime": -1,
  "minTime": 300
};
@NgModule({
  declarations: [
    AppComponent,   
    MainPageComponent, 
    HeaderComponent,
    FooterComponent, 
    LeftmenuComponent,
    ProcedureComponent
  ], 
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AmexioDataModule,
    AmexioWidgetModule,
    AmexioChartsModule,
    AmexioEnterpriseModule,
    AngularFontAwesomeModule,
    BrowserAnimationsModule,
    AngularSplitModule.forRoot(),
    RouterModule.forRoot(appRoutes, { useHash: true }),
    PrismModule,
    TreeModule,
    CommonModule,
    ToastModule,
    ButtonModule,
    ContextMenuModule,
    TabViewModule,
    CodeHighlighterModule,
    AccordionModule,
    TableModule,
    NgbModule,
    ProgressBarModule,
    BreadcrumbModule,
    HttpClientModule,
    NgxUiLoaderModule.forRoot(ngxUiLoaderConfig),
    NgxUiLoaderHttpModule,
    ModalModule.forRoot(),
    AngularMultiSelectModule,
    TablesModule,
    DatabaseModule,
  /*  ProcedureModule*/
  ],
  providers:
    [
      {
        provide: LocationStrategy,
        useClass: HashLocationStrategy
      }, 
      TreeDragDropService,
      MessageService,
      ProcedureService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
