import { Component } from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent {

  ngAfterViewInit() {
    this.javaS();
  }


  javaS() {
    console.log("using script.js for toggle button");
  
    // Select the sidebar toggle button
    const sidebarToggle1 = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle1) {
      // Check localStorage for persisted toggle state
      if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        document.body.classList.toggle('sb-sidenav-toggled');
      }
  
      // Add event listener to toggle sidebar on click
      sidebarToggle1.addEventListener('click', event => {
        event.preventDefault();
        document.body.classList.toggle('sb-sidenav-toggled');
        // Optionally persist the toggle state
        localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled').toString());
      });
    }
  }


}
