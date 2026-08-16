import { Routes } from '@angular/router';
import { TodoDisplayerComponent } from './components/todo-displayer.component';

export const routes: Routes = [
  {
    path: '',
    component: TodoDisplayerComponent,
    pathMatch: 'full'
  }
];
