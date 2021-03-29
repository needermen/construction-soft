import { browser, element, by } from 'protractor';

export class ManhattanPage {
  navigateTo() {
    return browser.get('/');
  }
}
