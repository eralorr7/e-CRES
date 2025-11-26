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
  
 
    const sidebarToggle1 = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle1) {

      if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        document.body.classList.toggle('sb-sidenav-toggled');
      }
  

      sidebarToggle1.addEventListener('click', event => {
        event.preventDefault();
        document.body.classList.toggle('sb-sidenav-toggled');
        localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled').toString());
      });
    }
  }


}
