import { Component } from "@angular/core";
import { NavigationEnd, Router } from "@angular/router";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styles: [],
})
export class AppComponent {
  isNotFoundRoute: boolean = false;
  isBadRequestRoute: boolean = false;
  
  constructor(private router: Router) {
    // Subscribe to router events to check for NotFoundComponent route
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isNotFoundRoute = this.router.url === '/not-found';
      }
      if (event instanceof NavigationEnd) {
        this.isBadRequestRoute = this.router.url === '/bad-request';
      }
    });
  }
}
