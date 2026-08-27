import { Injectable, computed, inject, signal } from '@angular/core';
import { finalize } from 'rxjs';
import {
  ApplicationStatus,
  JobApplicationClient,
  JobApplicationDetailDto,
  JobApplicationDto,
  CreateJobApplicationRequest,
  UpdateJobApplicationRequest,
  UpdateJobApplicationStatusRequest,
} from '../../generated/api';

@Injectable({ providedIn: 'root' })
export class JobApplicationService {
  private readonly api = inject(JobApplicationClient);

  private readonly _applications = signal<JobApplicationDto[]>([]);
  private readonly _selectedApplication = signal<JobApplicationDetailDto | null>(null);
  private readonly _loading = signal(false);
  private readonly _error = signal<string | null>(null);

  readonly applications = this._applications.asReadonly();
  readonly selectedApplication = this._selectedApplication.asReadonly();
  readonly loading = this._loading.asReadonly();
  readonly error = this._error.asReadonly();

  readonly applicationCount = computed(() => this._applications().length);
  readonly statusCounts = computed(() => {
    const counts = new Map<ApplicationStatus, number>();

    for (const value of Object.values(ApplicationStatus)) {
      counts.set(value, 0);
    }

    for (const app of this._applications()) {
      const status = app.status ?? ApplicationStatus.Draft;
      counts.set(status, (counts.get(status) ?? 0) + 1);
    }

    return counts;
  });

  loadApplications(status?: ApplicationStatus | null, companyId?: string | null): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .getAll(status ?? undefined, companyId ?? undefined)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: (applications) => this._applications.set(applications),
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  loadApplicationById(id: string): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .getById(id)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: (application) => this._selectedApplication.set(application),
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  createApplication(request: CreateJobApplicationRequest): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .create(request)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: (application) => {
          this._applications.update((apps) => [application, ...apps]);
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  updateApplication(id: string, request: UpdateJobApplicationRequest): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .update(id, request)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: () => {
          this._applications.update((apps) =>
            apps.map((app) =>
              app.id === id
                ? {
                    ...app,
                    positionTitle: request.positionTitle,
                    appliedOn: request.appliedOn,
                    jobPostingUrl: request.jobPostingUrl,
                    location: request.location,
                    employmentType: request.employmentType,
                    workModel: request.workModel,
                    salaryMin: request.salaryMin,
                    salaryMax: request.salaryMax,
                    salaryCurrency: request.salaryCurrency,
                    source: request.source,
                    notes: request.notes,
                    updatedAtUtc: new Date(),
                  }
                : app,
            ),
          );
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  changeStatus(id: string, request: UpdateJobApplicationStatusRequest): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .changeStatus(id, request)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: () => {
          this._applications.update((apps) =>
            apps.map((app) =>
              app.id === id
                ? {
                    ...app,
                    status: request.status,
                    updatedAtUtc: new Date(),
                  }
                : app,
            ),
          );

          const current = this._selectedApplication();
          if (current && current.id === id) {
            this._selectedApplication.set({
              ...current,
              status: request.status,
              updatedAtUtc: new Date(),
            });
          }
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  deleteApplication(id: string): void {
    this._loading.set(true);
    this._error.set(null);

    this.api
      .delete(id)
      .pipe(finalize(() => this._loading.set(false)))
      .subscribe({
        next: () => {
          this._applications.update((apps) => apps.filter((app) => app.id !== id));
          const current = this._selectedApplication();
          if (current?.id === id) {
            this._selectedApplication.set(null);
          }
        },
        error: (error) => this._error.set(this.extractErrorMessage(error)),
      });
  }

  private extractErrorMessage(error: unknown): string {
    if (typeof error === 'object' && error !== null) {
      const value = error as {
        message?: string;
        title?: string;
        detail?: string;
        error?: { message?: string; title?: string; detail?: string };
      };

      if (value.error) {
        if (value.error.detail) return value.error.detail;
        if (value.error.title) return value.error.title;
        if (value.error.message) return value.error.message;
      }

      if (value.detail) return value.detail;
      if (value.title) return value.title;
      if (value.message) return value.message;
    }

    return 'Something went wrong while loading job applications.';
  }
}
