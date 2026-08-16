import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Todo, TodoClient } from '../generated/api';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-todo-displayer',
  imports: [DatePipe],
  template: `
    <section>
      <h2>Todos</h2>
      @for (todo of todos(); track todo.id) {
        <div>
          <h3>{{ todo.title }}</h3>
          <p>Due by: {{ todo.dueBy | date }}</p>
          <p>Status: {{ todo.isComplete ? 'Complete' : 'Incomplete' }}</p>
        </div>
      }
    </section>
  `,
  styles: [``],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TodoDisplayerComponent {
  private readonly todoClient = inject(TodoClient);
  readonly todos = signal<Todo[]>([]);

  constructor() {
    this.todoClient
      .getTodos()
      .pipe(takeUntilDestroyed())
      .subscribe((todos) => {
        this.todos.set(todos);
      });
  }
}
