import { Admin2FrontendPage } from './app.po';

describe('admin2-frontend App', function() {
  let page: Admin2FrontendPage;

  beforeEach(() => {
    page = new Admin2FrontendPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
