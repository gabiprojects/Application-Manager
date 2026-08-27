import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApplicationStatus, CreateJobApplicationRequest, EmploymentType, WorkModel } from '../../generated/api';
import { JobApplicationService } from './job-application.service';

@Component({
  selector: 'app-job-application-new-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="mx-auto max-w-3xl rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <div class="mb-6">
        <p class="text-sm font-medium text-sky-700">New application</p>
        <h1 class="text-3xl font-bold text-slate-900">Add a job application</h1>
      </div>

      <form [formGroup]="form" (ngSubmit)="submit()" class="space-y-5">
        <div class="grid gap-5 md:grid-cols-2">
          <label class="block text-sm font-medium text-slate-700">
            Company ID
            <input formControlName="companyId" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500" />
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Position title
            <input formControlName="positionTitle" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500" />
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Status
            <select formControlName="status" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500">
              <option *ngFor="let status of statusOptions" [value]="status">{{ status }}</option>
            </select>
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Applied on
            <input type="date" formControlName="appliedOn" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500" />
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Location
            <input formControlName="location" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500" />
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Source
            <input formControlName="source" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500" />
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Employment type
            <select formControlName="employmentType" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500">
              <option value="">Select</option>
              <option *ngFor="let type of employmentTypeOptions" [value]="type">{{ type }}</option>
            </select>
          </label>

          <label class="block text-sm font-medium text-slate-700">
            Work model
            <select formControlName="workModel" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500">
              <option value="">Select</option>
              <option *ngFor="let model of workModelOptions" [value]="model">{{ model }}</option>
            </select>
          </label>
        </div>

        <label class="block text-sm font-medium text-slate-700">
          Job posting URL
          <input formControlName="jobPostingUrl" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500" />
        </label>

        <label class="block text-sm font-medium text-slate-700">
          Notes
          <textarea formControlName="notes" rows="4" class="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2 outline-none focus:border-sky-500"></textarea>
        </label>

        <div class="flex justify-end gap-3">
          <button type="button" class="rounded-xl border border-slate-300 px-4 py-2 text-sm font-medium text-slate-700 hover:bg-slate-50">Cancel</button>
          <button type="submit" [disabled]="form.invalid" class="rounded-xl bg-sky-600 px-4 py-2 text-sm font-medium text-white hover:bg-sky-700 disabled:cursor-not-allowed disabled:bg-slate-400">
            Save
          </button>
        </div>
      </form>
    </div>
  `,
})
export class JobApplicationNewPage {
  private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly jobApplicationService = inject(JobApplicationService);

  readonly statusOptions = Object.values(ApplicationStatus);
  readonly employmentTypeOptions = Object.values(EmploymentType);
  readonly workModelOptions = Object.values(WorkModel);

  readonly form = this.fb.group({
    companyId: ['', Validators.required],
    positionTitle: ['', Validators.required],
    status: [ApplicationStatus.Draft, Validators.required],
    appliedOn: [new Date().toISOString().slice(0, 10), Validators.required],
    location: [''],
    source: [''],
    employmentType: [''],
    workModel: [''],
    jobPostingUrl: [''],
    notes: [''],
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const payload: CreateJobApplicationRequest = {
      companyId: this.form.value.companyId!,
      positionTitle: this.form.value.positionTitle!,
      status: this.form.value.status ?? ApplicationStatus.Draft,
      appliedOn: new Date(this.form.value.appliedOn!),
      location: this.form.value.location || undefined,
      source: this.form.value.source || undefined,
      employmentType: (this.form.value.employmentType as EmploymentType | '') || undefined,
      workModel: (this.form.value.workModel as WorkModel | '') || undefined,
      jobPostingUrl: this.form.value.jobPostingUrl || undefined,
      notes: this.form.value.notes || undefined,
    };

    this.jobApplicationService.createApplication(payload);
    this.router.navigate(['/job-applications']);
  }
}
