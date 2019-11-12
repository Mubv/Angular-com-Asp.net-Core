import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Observable } from "rxjs";
import { UserService } from "../user.service";
import { User } from "../user";
import { DateAdapter, MAT_DATE_FORMATS } from "@angular/material/core";
import { AppDateAdapter, APP_DATE_FORMATS } from "src/app/format-datepicker";

// links: https://www.c-sharpcorner.com/article/crud-operation-in-angular-7-using-web-api/
// https://dzone.com/articles/use-aspnet-web-api-and-angular-to-build-a-simple-a
// http://www.macoratti.net/18/05/aspcore_cors1.htm

@Component({
  selector: "app-user",
  templateUrl: "./user.component.html",
  styleUrls: ["./user.component.css"],
  providers: [
    { provide: DateAdapter, useClass: AppDateAdapter },
    { provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS }
  ]
})
export class UserComponent implements OnInit {
  dataSaved = false;
  userForm: any;
  allusers: Observable<User[]>;
  userIdUpdate = null;
  massage = null;
  usuarios: Array<User>;

  constructor(
    private formbulider: FormBuilder,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.userForm = this.formbulider.group({
      Nome: ["", [Validators.required]],
      CPF: ["", [Validators.required]],
      Email: ["", [Validators.required]],
      DataNascimento: ["", [Validators.required]],
      CEP: ["", [Validators.required]],
      Cidade: ["", [Validators.required]],
      Estado: ["", [Validators.required]]
    });
    this.loadAllusers();
  }

  loadAllusers() {
    this.allusers = this.userService.getAllUser();
  }

  onFormSubmit() {
    this.dataSaved = false;
    const user = this.userForm.value;
    this.Createuser(user);
    this.userForm.reset();
  }

  loaduserToEdit(userId: string) {
    this.userService.getUserById(userId).subscribe(user => {
      this.massage = null;
      this.dataSaved = false;
      this.userIdUpdate = user.id;
      this.userForm.controls["Nome"].setValue(user.nome);
      this.userForm.controls["CPF"].setValue(user.cpf);
      this.userForm.controls["Email"].setValue(user.email);
      this.userForm.controls["DataNascimento"].setValue(user.dataNascimento);
      this.userForm.controls["CEP"].setValue(user.cep);
      this.userForm.controls["Cidade"].setValue(user.cidade);
      this.userForm.controls["Estado"].setValue(user.estado);
    });
  }

  Createuser(user: User) {
    if (this.userIdUpdate == null) {
      this.userService.createUser(user).subscribe(() => {
        this.dataSaved = true;
        this.loadAllusers();
        this.userIdUpdate = null;
        this.userForm.reset();
      });
    } else {
      user.id = this.userIdUpdate;
      this.userService.updateUser(user).subscribe(() => {
        this.dataSaved = true;
        this.loadAllusers();
        this.userIdUpdate = null;
        this.userForm.reset();
      });
    }
  }

  deleteuser(user: User) {
    if (
      confirm(
        "Este usuário selecionado será permanentemente apagado da base! Deseja prosseguir ?"
      )
    ) {
      this.userService.deleteUserById(user).subscribe(() => {
        this.dataSaved = true;
        this.loadAllusers();
        this.userIdUpdate = null;
        this.userForm.reset();
      });
    }
  }

  formatarCPF() {
    var parametro = this.userForm.controls["CPF"].value;

    if (parametro.length < 11 || parametro.length > 11) {
      confirm("CPF Inválido.Verifique");
      this.userForm.controls["CPF"].setValue("");
      return;
    }

    var result = Number(parametro);
    if(isNaN(result))
    {
      confirm("CPF Inválido.Verifique");
      this.userForm.controls["CPF"].setValue("");
      return;
    }

    var cpf = parametro[0] + parametro[1] + parametro[2] + ".";
    cpf += parametro[3] + parametro[4] + parametro[5] + ".";
    cpf += parametro[6] + parametro[7] + parametro[8] + "-";
    cpf += parametro[9] + parametro[10];
    this.userForm.controls["CPF"].setValue(cpf);
  }

  formatarCEP() {
    var parametro = this.userForm.controls["CEP"].value;

    if (parametro.length < 8 || parametro.length > 8) {
      confirm("CEP Inválido.Verifique");
      this.userForm.controls["CEP"].setValue("");
      return;
    }

    var result = Number(parametro);
    if(isNaN(result))
    {
      confirm("CEP Inválido.Verifique");
      this.userForm.controls["CEP"].setValue("");
      return;
    }
    var cep =
      parametro[0] +
      parametro[1] +
      parametro[2] +
      parametro[3] +
      parametro[4] +
      "-" +
      parametro[5] +
      parametro[6] +
      parametro[7];
    this.userForm.controls["CEP"].setValue(cep);
  }

  resetForm() {
    this.userForm.reset();
    this.massage = null;
    this.dataSaved = false;
  }
}
