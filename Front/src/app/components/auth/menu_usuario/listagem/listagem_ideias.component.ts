import { Component, TemplateRef } from '@angular/core';
import { IdeiasPagedResult, IdeiaModel, IdeiaPostParamModel, IdeiaPutParamModel } from '../../../../models/ideia.model';
import { PageModel } from '../../../../models/page.model';
import { IdeiaService } from '../../../../providers/ideia.service';
import { Global } from '../../../../providers/global.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { NgxSpinnerService } from "ngx-spinner";
import { NotifierService } from 'angular-notifier';

@Component({
  selector: 'app-auth-menu-usuarios-listagem-ideias',
  templateUrl: `listagem_ideias.component.html`,
  styleUrls: ['listagem_ideias.component.scss'],
  providers: [IdeiaService]
})
export class MenuUsuarioIdeiasComponent {
  modalRef: BsModalRef = new BsModalRef();
  modalRefExclusao: BsModalRef = new BsModalRef();
  config: PerfectScrollbarConfigInterface = {};
  ColumnMode = ColumnMode;
  page = new PageModel();
  ideias: Array<IdeiaModel> = new Array<IdeiaModel>();
  colunas: object[] = [{ name: 'id' }, { name: 'desIdeia' }, { name: 'desMotivoInvestir' }, { name: 'indInteresseCompartilhar' }, { name: 'indNivelDesenvolvimento' }, { name: 'indNivelSigilo' }, { name: 'indAprovado' }];
  changeText: boolean = false;
  ideia: IdeiaModel = new IdeiaModel();
  idExclusao: string = '';

  constructor(private global: Global,
    private ideiaService: IdeiaService,
    private modalService: BsModalService,
    private notifierService: NotifierService,
    private spinner: NgxSpinnerService) {
    this.page.pageNumber = 0;
    this.page.size = 10;
  }

  ngOnInit() {
    this.setPage({ offset: 0 });
    this.getAllPaged();
  }

  // List
  setPage(pageInfo: any) {
    this.page.pageNumber = pageInfo.offset;
    this.getAllPaged();
  }

  getAllPaged() {
    this.spinner.show();
    this.ideiaService.getAllPagedByUser(this.page.pageNumber + 1, this.page.size, this.global.getLoggedUser().id)
      .subscribe((data: IdeiasPagedResult) => {
        this.spinner.hide();
        this.ideias = data.results;
        this.page.totalElements = data.rowCount;
        this.page.totalPages = data.pageCount;
      });
  }

  getInteresseCompartilhar(indTipo: string) {
    if (indTipo == "2") {
      return "Quero divulgar uma solução para alguém que esteja disposto a trabalhar nela";
    } else if (indTipo == "3") {
      return "Foi apenas algo que pensei";
    } else {
      return "Quero encontrar alguém que me ajude a colocá-la em prática";
    }
  }

  getNivelDesenvolvimento(indTipo: string) {
    if (indTipo == "2") {
      return "Tenho um projeto para executar esta ideia";
    } else if (indTipo == "3") {
      return "Já tenho um produto desenvolvido";
    } else if (indTipo == "3") {
      return "Já tenho um pedido de patente";
    } else {
      return "É apenas uma ideia";
    }
  }

  getStringRezise(str: string) {
    if (str && str.length > 150) {
      return str.substring(0, 150) + "...";
    }

    return str;
  }

  // Add or Change
  openModalNew(template: TemplateRef<any>) {
    this.ideia = new IdeiaModel();
    this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-lg' }));
  }

  openModalChange(id: string, template: TemplateRef<any>) {
    this.spinner.show();
    this.ideiaService.get(id)
      .subscribe((data: IdeiaModel) => {
        this.spinner.hide();
        this.ideia = data;
        this.modalRef = this.modalService.show(template, Object.assign({}, { class: 'modal-lg' }));
      });
  }

  delete(id: string, template: TemplateRef<any>) {
    this.idExclusao = id;
    this.modalRefExclusao = this.modalService.show(template, Object.assign({}, { class: 'modal-md' }));
  }

  continuar() {
    this.spinner.show();
    this.ideiaService.delete(this.idExclusao)
      .subscribe((data: any) => {
        this.notifierService.notify('success', 'Idéia excluída com sucesso');
        this.modalRefExclusao.hide();
        this.spinner.hide();
        this.getAllPaged();
      });
  }

  salvar() {
    let isError = false;
    if (!this.ideia.desIdeia || this.ideia.desIdeia.trim() == '') {
      this.notifierService.notify('error', 'Informe a descrição da idéia.');
      isError = true;
    }

    if (!this.ideia.indInteresseCompartilhar || this.ideia.indInteresseCompartilhar.trim() == '' || this.ideia.indInteresseCompartilhar.trim() == '0') {
      this.notifierService.notify('error', 'Informe qual o interesse em compratilhar.');
      isError = true;
    }

    if (!this.ideia.indNivelDesenvolvimento || this.ideia.indNivelDesenvolvimento.trim() == '' || this.ideia.indNivelDesenvolvimento.trim() == '0') {
      this.notifierService.notify('error', 'Informe o nível de desenvolvimento da idéia.');
      isError = true;
    }

    if (!isError) {
      this.spinner.show();
      if (this.ideia.id && this.ideia.id.trim() != '') {
        let ideiaPut = new IdeiaPutParamModel();
        ideiaPut.id = this.ideia.id;
        ideiaPut.desIdeia = this.ideia.desIdeia;
        ideiaPut.desMotivoInvestir = this.ideia.desMotivoInvestir;
        ideiaPut.indInteresseCompartilhar = this.ideia.indInteresseCompartilhar;
        ideiaPut.indNivelDesenvolvimento = this.ideia.indNivelDesenvolvimento;
        ideiaPut.indNivelSigilo = this.ideia.indNivelSigilo;
        ideiaPut.desComentario = this.ideia.desComentario;
        ideiaPut.usuarioId = this.global.getLoggedUser().id;

        this.ideiaService.put(ideiaPut)
          .subscribe((data: any) => {
            this.spinner.hide();
            this.notifierService.notify('success', 'Idéia alterada com sucesso');
            this.modalRef.hide();
            this.getAllPaged();
          });

      } else {
        let ideiaPost = new IdeiaPostParamModel();
        ideiaPost.desIdeia = this.ideia.desIdeia;
        ideiaPost.desMotivoInvestir = this.ideia.desMotivoInvestir;
        ideiaPost.indInteresseCompartilhar = this.ideia.indInteresseCompartilhar;
        ideiaPost.indNivelDesenvolvimento = this.ideia.indNivelDesenvolvimento;
        ideiaPost.indNivelSigilo = this.ideia.indNivelSigilo;
        ideiaPost.desComentario = this.ideia.desComentario;
        ideiaPost.usuarioId = this.global.getLoggedUser().id;

        this.ideiaService.post(ideiaPost)
          .subscribe((data: any) => {
            this.spinner.hide();
            this.notifierService.notify('success', 'Idéia cadastrada com sucesso');
            this.modalRef.hide();
            this.getAllPaged();
          });
      }
    }
  }
}