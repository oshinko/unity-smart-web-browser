#import <SafariServices/SafariServices.h>

extern "C" {
    void _open(const char *url) {
        NSString *nsStr = [NSString stringWithCString:url encoding:NSUTF8StringEncoding];
        NSURL *nsUrl = [NSURL URLWithString:nsStr];
        SFSafariViewController *safari = [[SFSafariViewController alloc] initWithURL:nsUrl];
        UIViewController *parent = UnityGetGLViewController();
        [parent presentViewController:safari animated:YES completion:nil];
    }
}
