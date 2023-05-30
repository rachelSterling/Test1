import { Component } from '@angular/core';
import { HttpService } from '../../services/http.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'email-form',
  templateUrl: './email-form.component.html',
  styleUrls: ['./email-form.component.scss']
})
export class EmailFormComponent {
  message: string = '';
  emailForm: FormGroup = new FormGroup({});
  errorMessage: string='';

  constructor(private httpService: HttpService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.emailForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  sendEmail(): void {
    this.message = '';
    this.errorMessage = '';

    if (this.emailForm.valid) {
      const email = this.emailForm.value.email;
      let request  = { Email: email };
      this.httpService.post('Account', request).subscribe(
        (res) => {
          this.message = res.date;
        },
        (error) => {
          console.log('Error:', error);
          this.errorMessage =error.error.message;
        }
      );
    }


  }
}
