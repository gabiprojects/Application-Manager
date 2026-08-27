import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="min-h-screen bg-slate-100 text-slate-800">
      <div class="mx-auto flex min-h-screen max-w-[1600px]">
        <aside class="w-72 border-r border-slate-200 bg-slate-950 text-slate-100">
          <div class="border-b border-slate-800 px-6 py-6">
            <h1 class="text-xl font-bold tracking-tight">Application Manager</h1>
          </div>

          <nav class="space-y-2 p-4">
            <a routerLink="/overview" routerLinkActive="bg-sky-600 text-white" [routerLinkActiveOptions]="{ exact: true }" class="flex items-center rounded-xl px-3 py-2.5 text-sm font-medium text-slate-200 transition hover:bg-slate-800 hover:text-white">
              Overview
            </a>
            <a routerLink="/companies" routerLinkActive="bg-sky-600 text-white" class="flex items-center rounded-xl px-3 py-2.5 text-sm font-medium text-slate-200 transition hover:bg-slate-800 hover:text-white">
              Companies
            </a>
            <a routerLink="/job-applications" routerLinkActive="bg-sky-600 text-white" class="flex items-center rounded-xl px-3 py-2.5 text-sm font-medium text-slate-200 transition hover:bg-slate-800 hover:text-white">
              Job Applications
            </a>
          </nav>
        </aside>

        <div class="flex-1">
          <header class="border-b border-slate-200 bg-white/90 px-6 py-4 backdrop-blur-sm">
            <div class="flex items-center justify-between">
              <div>
                <p class="text-xs font-semibold uppercase tracking-[0.2em] text-sky-700">Workspace</p>
                <h2 class="mt-1 text-lg font-semibold text-slate-900">Application Manager</h2>
              </div>
            </div>
          </header>

          <main class="p-6">
            <router-outlet />
          </main>
        </div>
      </div>
    </div>
  `,
})
export class AppShellComponent {}
