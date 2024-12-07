import { IconName, IconPrefix, fas } from '@fortawesome/free-solid-svg-icons';
import { ModuleOption } from './module-option';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { FaIconLibrary } from '@fortawesome/angular-fontawesome';

export class Module {
  id = 0;
  name= '';
  subName = '';
  active = false;
  open = false;
  iconPrefix: IconPrefix = 'fas';
  iconName: IconName = 'cube';
  order = 0;

  get icon(): IconProp {
    const faIcon = new FaIconLibrary();
    faIcon.addIconPacks(fas);
    const inconValid = faIcon.getIconDefinition(this.iconPrefix, this.iconName);

    return inconValid !== null ? [this.iconPrefix, this.iconName] : ['fas', 'cube'];
  }

  options: ModuleOption[] = [];
}
