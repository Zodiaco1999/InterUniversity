import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Module } from '../../models/module';
import { MenuService } from '../../services/menu.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  @Output() mostrarMenu: EventEmitter<boolean> = new EventEmitter<boolean>();
  isCollapsed = false;
  idModule = 0;
  modules: Module[] = [];

  constructor(
    protected menuService: MenuService)
    {}

  ngOnInit(): void {
    this.modules = this.menuService.loadModules();
  }


  module_click(idModule: number) {
    const mod = this.modules.find((ele) => ele.id === idModule);
    if (mod)
      mod.open = !mod.open;
  }

  option_click(idModule: number, idOption: number) {
    const mod = this.modules.find((ele) => ele.id === idModule)!;
    mod.active = true;
    mod.options.forEach((f) => (f.active = false));

    const option = mod.options.find((a) => a.id === idOption)!;
    option.active = true;

    return (this.idModule = idModule);
  }

  activar() {
    this.mostrarMenu.emit(false);
  }

}
