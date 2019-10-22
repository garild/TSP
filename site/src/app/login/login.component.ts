import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';

import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertService, AuthenticationService } from '../services';

@Component({ templateUrl: './login.component.html'})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  returnUrl: string;
  message: any;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      team    : ['', Validators.required],
    });
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.authenticationService.login(this.f.username.value, this.f.password.value, this.f.team.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([ this.returnUrl]);
        },
        error => {
          this.alertService.error(error);
          this.message = error;
        });
  }
}
