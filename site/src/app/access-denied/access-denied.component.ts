import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-access-denied',
  templateUrl: './access-denied.component.html',
  styleUrls: ['./style.css']
})
export class AccessDeniedComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService,
    private router: Router) {
   }

  ngOnInit() {
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
}
}
