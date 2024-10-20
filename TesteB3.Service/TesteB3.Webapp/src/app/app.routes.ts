import { Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';

export const routes: Routes = [
  { path: '', pathMatch: 'prefix', redirectTo: 'main' },
  { path: 'main', component: MainPageComponent }
];
